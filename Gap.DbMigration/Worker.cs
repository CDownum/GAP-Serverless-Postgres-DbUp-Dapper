using System.Diagnostics;
using OpenTelemetry.Trace;

namespace GAP.DbMigration;

public class Worker(IConfiguration? configuration,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource SActivitySource = new(ActivitySourceName);

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = SActivitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            var migrator = new Migrator(configuration);

            migrator.MigrateSchema();
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
        return Task.CompletedTask;
    }
}