using System;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Answers;

public class AnswerDto: FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 科目Id（用于关联主表）
    /// </summary>
    public Guid SubjectId { get; set; }
    
    /// <summary>
    /// 科目的名称
    /// </summary>
    public string SubjectName { get; set; }

    /// <summary>
    /// 问题描述
    /// </summary>
    public string QuestionDescription { get; set; }

    /// <summary>
    /// 问题正确答案
    /// </summary>
    public string QuestionAnswer { get; set; }

    /// <summary>
    /// 问题表的Id（主表Id）
    /// </summary>
    public Guid QuestionId { get; set; }

    /// <summary>
    /// 您的回答
    /// </summary>
    public string YourAnswer { get; set; }
}