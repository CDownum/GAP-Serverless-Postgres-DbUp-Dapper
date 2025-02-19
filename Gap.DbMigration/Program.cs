using GAP.ServiceDefaults;
using Serilog;

namespace GAP.DbMigration;

internal class Program
{
    public static IConfigurationRoot? Configuration;

    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        // Create service collection
        Log.Information("Creating service collection");
        _ = ConfigureServices();

        builder.AddServiceDefaults();
        builder.Services.AddHostedService<Worker>();

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        var host = builder.Build();
        host.Run();
    }

    private static ServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();

        // Add logging
        services.AddSingleton(LoggerFactory.Create(builder =>
        {
            builder.AddSerilog(dispose: true);
        }));

        services.AddLogging();

        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();

        AddLogging(services, Configuration);

        // Add access to generic IConfigurationRoot
        services.AddSingleton<IConfiguration>(Configuration);

        return services;
    }

    public static IServiceCollection AddLogging(IServiceCollection services, IConfiguration? configuration)
    {
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(Log.Logger));
        services.AddSingleton<ILoggerProvider>(new Serilog.Extensions.Logging.SerilogLoggerProvider(Log.Logger));

        return services;
    }
}
