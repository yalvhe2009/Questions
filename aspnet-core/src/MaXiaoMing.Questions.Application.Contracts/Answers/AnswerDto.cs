using System;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Answers;

public class AnswerDto: FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 问题表的Id
    /// </summary>
    public Guid QuestionId { get; set; }

    /// <summary>
    /// 您的答案
    /// </summary>
    public string YourAnswer { get; set; }
}