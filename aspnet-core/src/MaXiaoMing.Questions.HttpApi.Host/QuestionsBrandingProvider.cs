using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MaXiaoMing.Questions;

[Dependency(ReplaceServices = true)]
public class QuestionsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Questions";
}
