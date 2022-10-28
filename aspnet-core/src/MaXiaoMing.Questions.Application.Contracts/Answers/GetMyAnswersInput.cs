using System;
using Volo.Abp.Application.Dtos;

namespace MaXiaoMing.Questions.Answers;

public class GetMyAnswersInput: PagedAndSortedResultRequestDto
{
    
    /// <summary>
    /// 科目Id
    /// </summary>
    public Guid? SubjectId { get; set; }

    /// <summary>
    /// 关键词
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 答题时间 起
    /// </summary>
    public DateTime? CreateTimeFrom { get; set; }

    /// <summary>
    /// 答题时间止
    /// </summary>
    public DateTime? CreateTimeTo { get; set; }
    
}