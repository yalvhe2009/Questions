using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Questions;
using MaXiaoMing.Questions.Questions.Dto;
using MaXiaoMing.Questions.Subjects;
using MaXiaoMing.Questions.Subjects.Dto;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace MaXiaoMing.Questions.Answers;

public class AnswerAppService: ApplicationService, IAnswerAppService
{
    private readonly IRepository<Subject, Guid> _subjectRepository;
    private readonly IRepository<Question, Guid> _questionRepository;
    private readonly IRepository<Answer, Guid> _answerRepository;
    private readonly IGuidGenerator _guidGenerator;

    public AnswerAppService(
        IRepository<Subject, Guid> subjectRepository,
        IRepository<Question, Guid> questionRepository,
        IRepository<Answer, Guid> answerRepository,
        IGuidGenerator guidGenerator
        )
    {
        _subjectRepository = subjectRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _guidGenerator = guidGenerator;
    }
    
    [Authorize]
    public async Task<ListResultDto<SubjectDto>> GetAllSubjectListAsync()
    {
        var queryable = await _subjectRepository.GetQueryableAsync();
        queryable = queryable
            .OrderBy(nameof(Subject.CreationTime) + " DESC");
        var subjects = await AsyncExecuter.ToListAsync(queryable);

        var subjectDtos = ObjectMapper.Map<List<Subject>, List<SubjectDto>>(subjects);
        return new ListResultDto<SubjectDto>(subjectDtos);
    }

    [Authorize]
    public async Task<ListResultDto<QuestionDto>> GetUnDoQuestionAsync(Guid subjectId, int count)
    {
        var questionQry = await _questionRepository.GetQueryableAsync();
        var answerQry = await _answerRepository.GetQueryableAsync();

        answerQry = answerQry.Where(x => x.CreatorId == CurrentUser.Id);

        var answers = await AsyncExecuter.ToListAsync(answerQry);
        var questionIds = answers.Select(x => x.QuestionId).Distinct().ToList();

        questionQry = questionQry
            .Where(x => x.SubjectId == subjectId)
            .Where(x => !questionIds.Contains(x.Id))
            .OrderByDescending(x => x.CreationTime)
            .Take(count);//只取出前count条

        var questionEntities = await AsyncExecuter.ToListAsync(questionQry);
        var questionDtos = ObjectMapper.Map<List<Question>, List<QuestionDto>>(questionEntities);
        return new ListResultDto<QuestionDto>(questionDtos);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    [Authorize]
    public async Task<ListResultDto<Guid>> SubmitYourAnswerAsync(List<CreateAnswerDto> input)
    {
        List<Guid> guids = new List<Guid>();

        foreach (var answerDto in input)
        {
            var question = await _questionRepository.FirstOrDefaultAsync( x => x.Id == answerDto.QuestionId);
            if (question == null)
            {
                throw new UserFriendlyException($"错误, 您要回答的问题不存在!");
            }

            var id = _guidGenerator.Create();
            guids.Add(id);
            var answer = new Answer(id, answerDto.QuestionId.Value, answerDto.YourAnswer);
            await _answerRepository.InsertAsync(answer);
        }

        return new ListResultDto<Guid>(guids);
    }

    [Authorize]
    public async Task<ListResultDto<AnswerDto>> GetAnalysisByAnswerIdsAsync(List<Guid> answersId)
    {
        var questionQry = await _questionRepository.GetQueryableAsync();
        var answerQry = await _answerRepository.GetQueryableAsync();
        var subjectQry = await _subjectRepository.GetQueryableAsync();

        answerQry = answerQry
            .Where(x => x.CreatorId == CurrentUser.Id)
            .Where(x => answersId.Contains(x.Id));

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
                CreationTime = answer.CreationTime //这道题是什么时候答的
            };
        var answerDtos = await AsyncExecuter.ToListAsync(qry);

        return new ListResultDto<AnswerDto>(answerDtos);
    }

    [Authorize]
    public async Task<PagedResultDto<AnswerDto>> GetMyAnswersAsync(GetMyAnswersInput input)
    {
        var questionQry = await _questionRepository.GetQueryableAsync();
        var answerQry = await _answerRepository.GetQueryableAsync();
        var subjectQry = await _subjectRepository.GetQueryableAsync();

        answerQry = answerQry
            .Where(x => x.CreatorId == CurrentUser.Id)
            .WhereIf(input.CreateTimeFrom.HasValue, x => input.CreateTimeFrom <= x.CreationTime)
            .WhereIf(input.CreateTimeTo.HasValue, x => x.CreationTime <= input.CreateTimeTo);

        questionQry = questionQry
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.QuestionDescription.Contains(input.Filter));

        subjectQry = subjectQry
            .WhereIf(input.SubjectId.HasValue, x => x.Id == input.SubjectId);
        
        var qry = from answer in answerQry
            join question in questionQry on answer.QuestionId equals question.Id
            join subject in subjectQry on question.SubjectId equals  subject.Id
            select new AnswerDto
            {
                Id = answer.Id,
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                QuestionDescription = question.QuestionDescription,
                QuestionAnswer = question.QuestionAnswer,
                QuestionId = answer.QuestionId,
                YourAnswer = answer.YourAnswer,
                CreationTime = answer.CreationTime //这道题是什么时候答的
            };

        var count = await AsyncExecuter.CountAsync(qry);
        qry = qry.Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .OrderBy(input.Sorting ?? nameof(Answer.CreationTime));

        var answerDtos = await AsyncExecuter.ToListAsync(qry);

        return new PagedResultDto<AnswerDto>(count, answerDtos);
    }
}