using MaXiaoMing.Questions.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace MaXiaoMing.Questions.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(QuestionsEntityFrameworkCoreModule),
    typeof(QuestionsApplicationContractsModule)
    )]
public class QuestionsDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
