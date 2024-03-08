using Hangfire;

namespace StockRich.Data.Jobs;

public class JobManager
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly CompanyDataSyncJob _companyDataSyncJob;

    public JobManager(IRecurringJobManager recurringJobManager, CompanyDataSyncJob companyDataSyncJob)
    {
        _recurringJobManager = recurringJobManager;
        _companyDataSyncJob = companyDataSyncJob;
    }

    public Task Start()
    {
        _recurringJobManager.AddOrUpdate(nameof(CompanyDataSyncJob), () => _companyDataSyncJob.Execute(),
            Cron.Minutely);
        return Task.CompletedTask;
    }
}