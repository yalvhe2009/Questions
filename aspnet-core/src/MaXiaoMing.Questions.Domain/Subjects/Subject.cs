using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MaXiaoMing.Questions.Subjects;

public class Subject: FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 科目名称
    /// </summary>
    public string Name { get; set; }

    public Subject(string name)
    {
        Name = name;
    }
    
    //ORM和序列化使用
    private Subject(){ }
}