using System.Reflection;
using DbUp;
using DbUp.Helpers;
using DbUp.Postgresql;
using GAP.DbMigration.Helpers;

namespace GAP.DbMigration;

public class Migrator
{
    public static IConfiguration? Configuration;
    private const string Schema = "gap";
    public Migrator(IConfiguration? configuration)
    {
        Configuration = configuration;
    }

    public virtual void MigrateSchema()
    {
        if (Configuration != null)
        {
            var connectionString = Configuration.GetValue<string>("Database:ConnectionString");
            var seedDatabase = Configuration.GetValue<bool>("Database:SeedDatabase");

            var builder = DeployChanges.To.PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetCallingAssembly(), s => s.Contains("Scripts.Script"))
                .LogToConsole();

            builder.Configure(
                c =>
                {
                    c.Journal = new PostgresqlTableJournal(() => c.ConnectionManager, () => c.Log, Schema,
                        "schema_version");
                    c.ScriptExecutor.ExecutionTimeoutSeconds = 240;
                });
            
            var upgrade = builder.Build();

            var result = upgrade.PerformUpgrade();

            if (seedDatabase)
            {
                if (connectionString != null) SeedDatabase(connectionString);
            }

            if (!result.Successful)
            {
                throw new MigrationException(result);
            }
        }
    }
        
    protected static void SeedDatabase(string connectionString)
    {
        var builder =
            DeployChanges.To.PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), s => s.Contains("TestDataScripts.TestData"))
                .JournalTo(new NullJournal())
                .LogToConsole();

        var upgrade = builder.Build();

        var result = upgrade.PerformUpgrade();

        if (!result.Successful)
        {
            throw new MigrationException(result);
        }
    }
}