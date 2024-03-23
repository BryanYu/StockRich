

using System.Runtime.CompilerServices;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StockRich.Data.Extension;
using StockRich.Data.Jobs;
using StockRich.Domain.Config;
using StockRich.Infrastructure.Data;

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
        builder.Services.Configure<OpenDataUrlConfig>(builder.Configuration.GetSection("OpenDataUrl"));


        builder.Services.AddTransient<CompanyDataSyncJob>();
        builder.Services.AddDbContext<StockRichContext>(
            option => option.UseNpgsql(builder.Configuration.GetConnectionString("StockRichConnection")),
            contextLifetime: ServiceLifetime.Transient, 
            optionsLifetime: ServiceLifetime.Transient);
        var app = builder.Build();
        app.UseHangfireDashboard();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapHangfireDashboard(); });
        app.UseJobStart();
        app.Run();
    }
}
