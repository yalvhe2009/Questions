using System;
using MaXiaoMing.Questions.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MaXiaoMing.Questions.Questions;

public class QuestionAppService: CrudAppService<
    Question,//实体类型
    QuestionDto,//响应数据列表里的每个元素的类型
    Guid,//主键类型
    PagedAndSortedResultRequestDto,//查询列表的输入参数类型
    CreateUpdateQuestionDto//创建或更新的Dto.类型
>,IQuestionAppService
{
    public QuestionAppService(IRepository<Question, Guid> repository) : base(repository)
    {
        GetPolicyName = QuestionsPermissions.Questions.查询;
        GetListPolicyName = QuestionsPermissions.Questions.查询;
        CreatePolicyName = QuestionsPermissions.Questions.创建;
        UpdatePolicyName = QuestionsPermissions.Questions.编辑;
        DeletePolicyName = QuestionsPermissions.Questions.删除;
    }
}