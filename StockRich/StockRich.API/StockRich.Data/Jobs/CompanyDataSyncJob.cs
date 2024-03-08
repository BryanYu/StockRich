using Hangfire.Common;

namespace StockRich.Data.Jobs;

public class CompanyDataSyncJob
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CompanyDataSyncJob(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public Task Execute()
    {
        Console.WriteLine(DateTime.Now);
        return Task.CompletedTask;
    }
}    
