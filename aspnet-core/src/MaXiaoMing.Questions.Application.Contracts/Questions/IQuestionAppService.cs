using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MaXiaoMing.Questions.Questions;

public interface IQuestionAppService: ICrudAppService<
    QuestionDto,//响应数据列表里的每个元素的类型
    Guid,//实体的主键类型
    PagedAndSortedResultRequestDto,//查询列表的输入参数类型
    CreateUpdateQuestionDto//创建或更新的Dto.类型
>
{
    
}