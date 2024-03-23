using Hangfire;
using StockRich.Data.Jobs;

namespace StockRich.Data.Extension;

public static class HangFireJobExtension
{
    public static IApplicationBuilder UseJobStart(this IApplicationBuilder builder)
    {
        var recurringJobManager = builder.ApplicationServices.GetService<IRecurringJobManager>();
        var companyDataSyncJob = builder.ApplicationServices.GetService<CompanyDataSyncJob>();
        recurringJobManager.AddOrUpdate(nameof(CompanyDataSyncJob), () => companyDataSyncJob.Execute(),
            Cron.Weekly);
        return builder;
    }
}