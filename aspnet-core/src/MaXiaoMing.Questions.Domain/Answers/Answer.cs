using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MaXiaoMing.Questions.Answers;

/// <summary>
/// 答题表
/// </summary>
public class Answer: FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 问题表的Id（主表Id）
    /// </summary>
    public Guid QuestionId { get; set; }

    /// <summary>
    /// 您的回答
    /// </summary>
    public string YourAnswer { get; set; }

    /// <summary>
    /// 构造函数，保证必须的关键字段都能赋上值
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="yourAnswer"></param>
    public Answer(Guid id, Guid questionId, string yourAnswer)
    {
        Id = id;
        QuestionId = questionId;
        YourAnswer = yourAnswer;
    }
    
    //ORM和序列号使用
    private Answer() { }
}