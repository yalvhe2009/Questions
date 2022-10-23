using System;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Subjects.Dto;

public class SubjectDto: FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 科目名称
    /// </summary>
    public string Name { get; set; }
}