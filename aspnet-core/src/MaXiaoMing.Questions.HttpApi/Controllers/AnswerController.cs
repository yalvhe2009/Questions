using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Answers;
using MaXiaoMing.Questions.Questions;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MaXiaoMing.Questions.Controllers;

[RemoteService]
[ControllerName("Answer（答题）")]
[Route("api/answers")]
public class AnswerController: AbpController, IAnswerAppService
{
    private readonly IAnswerAppService _answerAppService;

    public AnswerController(IAnswerAppService answerAppService)
    {
        _answerAppService = answerAppService;
    }
    
    [HttpGet]
    [Route("un-do-question/{subjectId}")]
    public async Task<ListResultDto<QuestionDto>> GetUnDoQuestionAsync(Guid subjectId, [Required] [Range(1, 50)] int count)
    {
        return await _answerAppService.GetUnDoQuestionAsync(subjectId, count);
    }

    [HttpPost]
    public async Task<ListResultDto<Guid>> SubmitYourAnswerAsync(List<CreateAnswerDto> input)
    {
        return await _answerAppService.SubmitYourAnswerAsync(input);
    }

    [HttpGet]
    [Route("analysis-by-ids")]
    public async Task<ListResultDto<AnswerDto>> GetAnalysisByAnswerIdsAsync(List<Guid> answerIds)
    {
        return await _answerAppService.GetAnalysisByAnswerIdsAsync(answerIds);
    }

    [HttpGet]
    [Route("my-answers")]
    public async Task<PagedResultDto<AnswerDto>> GetMyAnswersAsync(GetMyAnswersInput input)
    {
        return await _answerAppService.GetMyAnswersAsync(input);
    }
}