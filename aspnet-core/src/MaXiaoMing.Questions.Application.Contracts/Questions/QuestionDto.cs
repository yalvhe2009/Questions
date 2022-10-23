using System;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Questions;

public class QuestionDto: FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 科目Id
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// 问题描述
    /// </summary>
    public string QuestionDescription { get; set; }

    /// <summary>
    /// 问题正确答案
    /// </summary>
    public string QuestionAnswer { get; set; }
}