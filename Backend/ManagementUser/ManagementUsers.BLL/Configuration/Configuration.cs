namespace ManagementUsers.BLL.Configuration
{
    public static class Configuration
    {
        public const int DefaultStatusCode = 200;

        public static string ConnectionString { get; set; } = string.Empty;
        public static string CorsPolicyName = "policy";
    }
}
