using MaXiaoMing.Questions.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MaXiaoMing.Questions;

[DependsOn(
    typeof(QuestionsEntityFrameworkCoreTestModule)
    )]
public class QuestionsDomainTestModule : AbpModule
{

}
