namespace MaXiaoMing.Questions.Permissions;

public static class QuestionsPermissions
{
    public const string GroupName = "码小明题库";
    
    public static class Subjects
    {
        public const string Default = GroupName + ".科目";
        public const string 查询 = Default + ".查询";
        public const string 编辑 = Default + ".编辑";
        public const string 删除 = Default + ".删除";
        public const string 创建 = Default + ".创建";
    }
    
    public static class Questions
    {
        public const string Default = GroupName + ".题目";
        public const string 查询 = Default + ".查询";
        public const string 编辑 = Default + ".编辑";
        public const string 删除 = Default + ".删除";
        public const string 创建 = Default + ".创建";
    }

    public static class Answers
    {
        public const string Default = GroupName + ".答题";
    }
    
}
