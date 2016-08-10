using System.Data.Entity.Migrations;

namespace Cloud.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Cloud.EntityFramework.CloudDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Cloud";
        }

        protected override void Seed(Cloud.EntityFramework.CloudDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
