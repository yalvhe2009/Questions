using MaXiaoMing.Questions.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MaXiaoMing.Questions.Permissions;

public class QuestionsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(QuestionsPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(QuestionsPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<QuestionsResource>(name);
    }
}
