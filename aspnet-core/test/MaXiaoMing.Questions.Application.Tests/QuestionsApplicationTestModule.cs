using Volo.Abp.Modularity;

namespace MaXiaoMing.Questions;

[DependsOn(
    typeof(QuestionsApplicationModule),
    typeof(QuestionsDomainTestModule)
    )]
public class QuestionsApplicationTestModule : AbpModule
{

}
