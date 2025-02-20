using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using GAP.Api.Extensions;
using Serilog;
using GAP.Core.Database;
using GAP.Core.Database.Repository;
using System.Text.Json.Serialization;
using GAP.Core;
using GAP.Api.Functions.Users.Services;

namespace GAP.Api
{
    internal class Program
    {
        private static Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(h =>
                {
                    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    h.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureAppConfiguration(x =>
                {
                    x.AddCommandLine(args);
                    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    x.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureFunctionsWebApplication()
                .ConfigureServices((hostContext, services) =>
                {
                    AddLogging(services, hostContext.Configuration);
                    ConfigureOptions(services, hostContext.Configuration);
                })
                .Build();

            return host.RunAsync();
        }

        public static IServiceCollection AddLogging(IServiceCollection services, IConfiguration configuration)
        {
            var appId = configuration.GetValue<string>("Logs_ApplicationId");
            var workspaceId = configuration.GetValue<string>("Logs_WorkspaceId");
            var authenticationId = configuration.GetValue<string>("Logs_AuthenticationId");
            var env = configuration.GetValue<string>("Logs_Environment");

            Log.Logger = Log.Logger.CreateNewLogger(workspaceId, authenticationId, appId, env);

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(Log.Logger));
            services.AddSingleton<ILoggerProvider>(new Serilog.Extensions.Logging.SerilogLoggerProvider(Log.Logger));

            return services;
        }

        private static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration.GetValue<string>("Database:ConnectionString");

            services.Configure<JsonSerializerOptions>(configureOptions: options =>
            {
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGapRepository, GapRepository>();
            services.AddScoped<IUserRepository, GapRepository>();
            services.AddScoped<ISqlWrapper, SqlWrapper>();
            services.AddScoped(_ => new DbConnectionSettings()
            {
                Database = database ?? throw new InvalidOperationException("Missing DB connection")
            });

            services.AddAutoMapper(typeof(Program).Assembly);
        }
    }
}
