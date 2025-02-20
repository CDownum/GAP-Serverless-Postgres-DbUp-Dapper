
using Aspire.Hosting;

namespace GAP.AppHost;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var username = builder.AddParameter("username", secret: true);
        var password = builder.AddParameter("password", secret: true);

        var postgres = builder.AddPostgres("postgres", username, password, 52106)
            .WithDataVolume(isReadOnly: false)
            .WithLifetime(ContainerLifetime.Persistent);

        var postgresDb = postgres.AddDatabase("postgresDb", "postgres");

        builder.AddProject<Projects.GAP_DbMigration>("DbMigration")
            .WithReference(postgres)
            .WaitFor(postgresDb);

        var api = builder.AddAzureFunctionsProject<Projects.GAP_Api>("Gap-Serverless-Api");

        builder.AddNpmApp("Portal", "../GAP.Portal/GAP.Portal", "dev")
            .WithReference(api)
            .WaitFor(api)
            .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
            .WithHttpEndpoint(5174, 5173, "PORT", "dev", true)
            .WithExternalHttpEndpoints()
            .PublishAsDockerFile();

        builder.Build().Run();
    }
}