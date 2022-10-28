using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Questions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MaXiaoMing.Questions.Answers;

public interface IAnswerAppService: IApplicationService
{
    // 获取N(N前端传入)条 S(S前端传入)科目下的未答题目. (联表查出未答记录) 
    Task<ListResultDto<QuestionDto>> GetUnDoQuestionAsync(Guid subjectId, int count);
    
    //提交答题(传入N条数据), 在DB中创建数据, 返回所有的答题记录(或只返Id就行)
    Task<ListResultDto<Guid>> SubmitYourAnswerAsync(List<CreateAnswerDto> input);
    
    //查看本次答题的解析: 传入答案Id列表, 列表找出问题, 返回问题, 正确答案, 用户答案, 做题时间等信息.
    Task<ListResultDto<AnswerDto>> GetAnalysisByAnswerIdsAsync(List<Guid> answerIds);

    //获取我的答题
    Task<PagedResultDto<AnswerDto>> GetMyAnswersAsync(GetMyAnswersInput input);
}