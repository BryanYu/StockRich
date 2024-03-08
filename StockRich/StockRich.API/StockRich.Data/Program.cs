

using Hangfire;
using Hangfire.PostgreSql;
using StockRich.Data.Jobs;

namespace StockRich.Data;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        builder.Services.AddHangfire(config =>
        {
            config.UsePostgreSqlStorage(config2 =>
                config2.UseNpgsqlConnection(configuration.GetConnectionString("HangfireConnection")));
        });

        
        builder.Services.AddHangfireServer();
        builder.Services.AddAuthorization();
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<JobManager>();
        builder.Services.AddSingleton<CompanyDataSyncJob>();
        
        var app = builder.Build();
        
        app.UseHangfireDashboard();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHangfireDashboard();
        });

        var jobManager = app.Services.GetService<JobManager>();
        jobManager.Start();
        
        app.Run();
    }
}