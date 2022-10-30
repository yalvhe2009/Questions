using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MaXiaoMing.Questions.Answers;

/// <summary>
/// 答题表
/// </summary>
public class Answer: FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 问题表的Id
    /// </summary>
    public Guid QuestionId { get; set; }

    /// <summary>
    /// 您的答案
    /// </summary>
    public string YourAnswer { get; set; }

    public Answer(Guid id, Guid questionId, string yourAnswer)
    {
        Id = id;
        QuestionId = questionId;
        YourAnswer = yourAnswer;
    }

    private Answer() {}
}