using System;
using System.Collections.Generic;
using System.Text;
using MaXiaoMing.Questions.Localization;
using Volo.Abp.Application.Services;

namespace MaXiaoMing.Questions;

/* Inherit your application services from this class.
 */
public abstract class QuestionsAppService : ApplicationService
{
    protected QuestionsAppService()
    {
        LocalizationResource = typeof(QuestionsResource);
    }
}
