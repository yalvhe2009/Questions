using System;
using System.ComponentModel.DataAnnotations;

namespace MaXiaoMing.Questions.Questions.Dto;

public class CreateUpdateQuestionDto
{
    /// <summary>
    /// 科目Id
    /// </summary>
    [Required(ErrorMessage = "请传入科目Id！")]
    public Guid? SubjectId { get; set; }

    /// <summary>
    /// 问题描述
    /// </summary>
    [Required(ErrorMessage = "请传入问题描述！")]
    [StringLength(QuestionConsts.MaxQuestionDescriptionLength)]
    public string QuestionDescription { get; set; }

    /// <summary>
    /// 问题正确答案
    /// </summary>
    [Required(ErrorMessage = "请传入正确答案！")]
    [StringLength(QuestionConsts.MaxQuestionAnswerLength)]
    public string QuestionAnswer { get; set; }
}