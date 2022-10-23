using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MaXiaoMing.Questions.Data;

/* This is used if database provider does't define
 * IQuestionsDbSchemaMigrator implementation.
 */
public class NullQuestionsDbSchemaMigrator : IQuestionsDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
