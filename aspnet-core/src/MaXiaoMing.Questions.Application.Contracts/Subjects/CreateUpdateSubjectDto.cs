using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Subjects;

public class CreateUpdateSubjectDto
{
    /// <summary>
    /// 科目名称
    /// </summary>
    [Required(ErrorMessage = "请传入名称！")]
    [StringLength(SubjectConsts.MaxSubjectNameLength)]
    public string Name { get; set; }
}