using System;
using MaXiaoMing.Questions.Questions.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MaXiaoMing.Questions.Questions;

public interface IQuestionAppService: ICrudAppService<QuestionDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateQuestionDto>
{
    
}