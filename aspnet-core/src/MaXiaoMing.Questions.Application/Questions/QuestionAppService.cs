using System;
using MaXiaoMing.Questions.Permissions;
using MaXiaoMing.Questions.Questions.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MaXiaoMing.Questions.Questions;

public class QuestionAppService: CrudAppService<Question, QuestionDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateQuestionDto>, IQuestionAppService
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