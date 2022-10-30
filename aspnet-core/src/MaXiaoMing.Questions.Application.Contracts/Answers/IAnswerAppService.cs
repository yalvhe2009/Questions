using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaXiaoMing.Questions.Questions.Dto;
using MaXiaoMing.Questions.Subjects.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MaXiaoMing.Questions.Answers;

public interface IAnswerAppService: IApplicationService
{
    Task<ListResultDto<SubjectDto>> GetAllSubjectListAsync();

    Task<ListResultDto<QuestionDto>> GetUnDoQuestionAsync(Guid subjectId, int count);

    Task<ListResultDto<Guid>> SubmitYourAnswerAsync(List<CreateAnswerDto> input);

    Task<ListResultDto<AnswerDto>> GetAnalysisByAnswerIdsAsync(List<Guid> answersId);

    Task<PagedResultDto<AnswerDto>> GetMyAnswersAsync(GetMyAnswersInput input);
}