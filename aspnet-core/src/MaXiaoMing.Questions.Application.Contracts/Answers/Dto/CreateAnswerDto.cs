using System;
using System.ComponentModel.DataAnnotations;
using MaXiaoMing.Questions.Subjects;

namespace MaXiaoMing.Questions.Answers;

public class CreateAnswerDto
{
    /// <summary>
    /// 问题表的Id
    /// </summary>
    [Required(ErrorMessage = "请传入问题Id!")]
    public Guid? QuestionId { get; set; }

    /// <summary>
    /// 您的答案
    /// </summary>
    [StringLength(AnswerConsts.MaxYourAnswerLength)]
    public string YourAnswer { get; set; }
}