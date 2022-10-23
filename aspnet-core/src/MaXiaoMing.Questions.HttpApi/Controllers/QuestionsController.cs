using MaXiaoMing.Questions.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MaXiaoMing.Questions.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class QuestionsController : AbpControllerBase
{
    protected QuestionsController()
    {
        LocalizationResource = typeof(QuestionsResource);
    }
}
