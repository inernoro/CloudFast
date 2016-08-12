namespace Cloud.Framework.Redis
{
    public static class RedisConfig
    {
        public static string ConnectionString { get; set; }

        public static string[] ConnStrings { get; set; }

        public static int Database { get; set; } = 0;

        public const int TimeDefaultValidTime = 1800;

    }
}