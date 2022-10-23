using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MaXiaoMing.Questions.Subjects;

public class Subject: FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 科目名称
    /// </summary>
    public String Name { get; set; }

    public Subject(string name)
    {
        Name = name;
    }
    
    private Subject()
    {
        
    }
}