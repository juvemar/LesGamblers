namespace LesGamblers.Web.App_Start
{
    using System.Data.Entity;

    using LesGamblers.Data;
    using LesGamblers.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<LesGamblersDbContext, Configuration>());
            //LesGamblersDbContext.Create().Database.Initialize(true);
        }
    }
}