using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Questions;
using MaXiaoMing.Questions.Subjects;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Linq;

namespace MaXiaoMing.Questions.Answers;

public class AnswerAppService: QuestionsAppService, IAnswerAppService
{
    private readonly IRepository<Subject, Guid> _subjectRepository;
    private readonly IRepository<Question, Guid> _questionRepository;
    private readonly IRepository<Answer, Guid> _answerRepository;
    private readonly IGuidGenerator _guidGenerator;

    public AnswerAppService(
        IRepository<Subject, Guid> subjectRepository,
        IRepository<Question, Guid> questionRepository,
        IRepository<Answer, Guid> answerRepository, 
        IGuidGenerator guidGenerator)
    {
        _subjectRepository = subjectRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _guidGenerator = guidGenerator;
    }


    [Authorize]//必须登录后才能访问该接口
    public async Task<ListResultDto<QuestionDto>> GetUnDoQuestionAsync(Guid subjectId, int count)
    {
        var questionQry = await _questionRepository.GetQueryableAsync();
        var answerQry = await _answerRepository.GetQueryableAsync();
        
        answerQry = answerQry.Where(x => x.CreatorId == CurrentUser.Id);//只查询当前登录用户做过的题目
        var answers = await AsyncExecuter.ToListAsync(answerQry);//当前用户已经答过的所有题目
        var questionIds = answers.Select(x => x.QuestionId).Distinct().ToList();

        questionQry = questionQry
                .Where(x => x.SubjectId == subjectId) //只查询传入科目的问题.
                .Where(x => !questionIds.Contains(x.Id)) //过滤掉当前用户已经答过的问题.
                .OrderByDescending(x => x.CreationTime) //按照录入时间排序, 先录入的题目可以被用户先答
                .Take(count) //只取前count条
            ;

        var questionEntities = await AsyncExecuter.ToListAsync(questionQry);
        var questions = ObjectMapper.Map<List<Question>, List<QuestionDto>>(questionEntities);
        return new ListResultDto<QuestionDto>(questions);
    }

    [Authorize]
    public async Task<ListResultDto<Guid>> SubmitYourAnswerAsync(List<CreateAnswerDto> input)
    {
        List<Guid> guids = new List<Guid>();
        foreach (var answerDto in input)
        {
            //判断传入的QuestionId是否合法
            var question = await _questionRepository.FirstOrDefaultAsync(x => x.Id == answerDto.QuestionId);
            if (question == null)
            {
                throw new UserFriendlyException($"错误, 您要回答的问题不存在!");
            }
            var id = _guidGenerator.Create();
            guids.Add(id);
            var answer = new Answer(id, answerDto.QuestionId, answerDto.YourAnswer);
            await _answerRepository.InsertAsync(answer);
        }

        return new ListResultDto<Guid>(guids);
    }

    [Authorize]
    public async Task<ListResultDto<AnswerDto>> GetAnalysisByAnswerIdsAsync(List<Guid> answerIds)
    {
        var answerQry = await _answerRepository.GetQueryableAsync();
        var questionQry = await _questionRepository.GetQueryableAsync();
        var subjectQry = await _subjectRepository.GetQueryableAsync();

        answerQry = answerQry
            .Where(x => x.CreatorId == CurrentUser.Id)//只获取当前登录用户的题目.
            .Where(x => answerIds.Contains(x.Id))//只获取传入Id list的数据
            ;

        var qry = from answer in answerQry
            join question in questionQry on answer.QuestionId equals question.Id
            join subject in subjectQry on question.SubjectId equals subject.Id
            select new AnswerDto
            {
                Id = answer.Id,
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                QuestionDescription = question.QuestionDescription,
                QuestionAnswer = question.QuestionAnswer,
                QuestionId = answer.QuestionId,
                YourAnswer = answer.YourAnswer,
                CreationTime = answer.CreationTime //这道题你是什么时候答的.
            };

        var answerDtos = await AsyncExecuter.ToListAsync(qry);
        return new ListResultDto<AnswerDto>(answerDtos);
    }

    [Authorize]
    public async Task<PagedResultDto<AnswerDto>> GetMyAnswersAsync(GetMyAnswersInput input)
    {
        var answerQry = await _answerRepository.GetQueryableAsync();
        var questionQry = await _questionRepository.GetQueryableAsync();
        var subjectQry = await _subjectRepository.GetQueryableAsync();

        answerQry = answerQry
            .Where(x => x.CreatorId == CurrentUser.Id) //只获取当前登录用户的题目.
            .WhereIf(input.CreateTimeFrom.HasValue, x => input.CreateTimeFrom <= x.CreationTime)
            .WhereIf(input.CreateTimeTo.HasValue, x => x.CreationTime <= input.CreateTimeTo)
            ;
        questionQry = questionQry
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.QuestionDescription.Contains(input.Filter));
        subjectQry = subjectQry
            .WhereIf(input.SubjectId.HasValue, x => x.Id == input.SubjectId);
        
        var qry = from answer in answerQry
            join question in questionQry on answer.QuestionId equals question.Id
            join subject in subjectQry on question.SubjectId equals subject.Id
            select new AnswerDto
            {
                Id = answer.Id,
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                QuestionDescription = question.QuestionDescription,
                QuestionAnswer = question.QuestionAnswer,
                QuestionId = answer.QuestionId,
                YourAnswer = answer.YourAnswer,
                CreationTime = answer.CreationTime //这道题你是什么时候答的.
            };
        var count = await AsyncExecuter.CountAsync(qry);
        qry = qry.Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .OrderBy(input.Sorting ?? nameof(Answer.CreationTime));
        
        
        var answerDtos = await AsyncExecuter.ToListAsync(qry);

        return new PagedResultDto<AnswerDto>(count, answerDtos);
    }
}