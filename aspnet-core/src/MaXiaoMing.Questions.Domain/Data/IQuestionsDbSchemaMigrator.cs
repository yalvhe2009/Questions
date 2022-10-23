using System.Threading.Tasks;

namespace MaXiaoMing.Questions.Data;

public interface IQuestionsDbSchemaMigrator
{
    Task MigrateAsync();
}
