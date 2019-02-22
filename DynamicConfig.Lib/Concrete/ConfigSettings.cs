namespace DynamicConfig.Lib.Concrete
{
    public static class ConfigSettings
    {
        public static string ApplicationName { get; set; } = "ServiceA";
        public static string ConnectionString { get; set; } = "";
        public static int RefreshTimerIntervalInMs { get; set; } = 5000;
    }
}