using System.ComponentModel.DataAnnotations;

namespace MaXiaoMing.Questions.Subjects.Dto;

public class CreateUpdateSubjectDto
{
    /// <summary>
    /// 科目名称
    /// </summary>
    [Required(ErrorMessage = "请输入名称！")]
    [StringLength(SubjectConsts.MaxSubjectNameLength)]
    public string Name { get; set; }
}