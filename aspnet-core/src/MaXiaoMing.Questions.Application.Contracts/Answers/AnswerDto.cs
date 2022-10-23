using System;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Answers;

public class AnswerDto: FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 问题表的Id（主表Id）
    /// </summary>
    public Guid QuestionId { get; set; }

    /// <summary>
    /// 您的回答
    /// </summary>
    public string YourAnswer { get; set; }
}