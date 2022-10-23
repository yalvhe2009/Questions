using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MaXiaoMing.Questions.Questions;

public class Question: FullAuditedAggregateRoot<Guid>
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

    public Question(Guid subjectId, string questionDescription, string questionAnswer)
    {
        SubjectId = subjectId;
        QuestionDescription = questionDescription;
        QuestionAnswer = questionAnswer;
    }

    private Question() { }
}