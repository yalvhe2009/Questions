using System;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Answers;

public class GetMyAnswersInput: PagedAndSortedResultRequestDto
{
    public Guid? SubjectId { get; set; }

    public string Filter { get; set; }

    public DateTime? CreateTimeFrom { get; set; }

    public DateTime? CreateTimeTo { get; set; }
}