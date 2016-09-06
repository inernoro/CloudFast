namespace Cloud.Framework.Cache.Redis
{
    public static class CacheConfigurage
    {
        public static string ConnectionString { get; set; }

        public static string[] ConnStrings { get; set; }

        public static int Database { get; set; } = 0;

        public const int TimeDefaultValidTime = 1800;

    }
}