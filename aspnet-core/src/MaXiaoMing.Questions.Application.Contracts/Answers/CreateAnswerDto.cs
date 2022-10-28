using System;

namespace MaXiaoMing.Questions.Answers;

public class CreateAnswerDto
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