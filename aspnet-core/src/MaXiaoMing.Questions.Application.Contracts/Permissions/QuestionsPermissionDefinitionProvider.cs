using MaXiaoMing.Questions.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MaXiaoMing.Questions.Permissions;

public class QuestionsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var 码小明题库 = context.AddGroup(QuestionsPermissions.GroupName);
        var 科目 = 码小明题库.AddPermission(QuestionsPermissions.Subjects.Default, new FixedLocalizableString("科目"));
        科目.AddChild(QuestionsPermissions.Subjects.创建, new FixedLocalizableString("创建"));
        科目.AddChild(QuestionsPermissions.Subjects.删除, new FixedLocalizableString("删除"));
        科目.AddChild(QuestionsPermissions.Subjects.查询, new FixedLocalizableString("查询"));
        科目.AddChild(QuestionsPermissions.Subjects.编辑, new FixedLocalizableString("编辑"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<QuestionsResource>(name);
    }
}
