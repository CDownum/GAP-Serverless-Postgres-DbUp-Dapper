using System.Net;
using Serilog;
using Serilog.Core;
using Serilog.Core.Enrichers;
using Serilog.Events;
using Serilog.Exceptions;

namespace GAP.Api.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger CreateNewLogger(this ILogger logger, string workspaceId, string authenticationId, string applicationId, string environment, bool logFile = false)
        {
            if (string.IsNullOrEmpty(environment) || environment.ToLower() == "local")
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.WithProperty("Application", applicationId)
                    .Enrich.WithProperty("Environment", environment)
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithMachineName()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console()
                    .CreateLogger();
            }

            if (logFile)
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.WithProperty("Application", applicationId)
                    .Enrich.WithProperty("Environment", environment)
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithMachineName()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Minute)
                    .WriteTo.Console()
                    .CreateLogger();
            }

            return new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.WithProperty("Application", applicationId)
                .Enrich.WithProperty("Environment", environment)
                .Enrich.FromLogContext()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithAssemblyName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static ILogger WithHttpStatus(this ILogger logger, HttpStatusCode statusCode)
        {
            return logger.ForContext(new List<ILogEventEnricher>
            {
                new PropertyEnricher("HttpStatus", $@"{statusCode} ({(int)statusCode})")
            });
        }
    }
}