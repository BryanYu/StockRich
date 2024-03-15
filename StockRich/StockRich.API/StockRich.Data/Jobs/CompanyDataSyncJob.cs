using Hangfire.Common;
using Microsoft.Extensions.Options;
using StockRich.Domain.Config;

namespace StockRich.Data.Jobs;

public class CompanyDataSyncJob
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenDataUrlConfig _openDataUrlConfig;
    private readonly ILogger<CompanyDataSyncJob> _logger;

    public CompanyDataSyncJob(IHttpClientFactory httpClientFactory, IOptions<OpenDataUrlConfig> openDataUrlOptions, ILogger<CompanyDataSyncJob> logger)
    {
        _httpClientFactory = httpClientFactory;
        _openDataUrlConfig = openDataUrlOptions.Value;
        _logger = logger;
    }
    
    public async Task Execute()
    {
        var dataContent = await FetchDataAsync();
    }

    private async Task<string> FetchDataAsync()
    {
        var url = $"{_openDataUrlConfig.TaiwanStockExchange}/t187ap03_L";
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(url, CancellationToken.None);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Fetch Data From {url} Error, HttpStatus:{response.StatusCode}");
            return string.Empty;
        }
        await using var stream = await response.Content.ReadAsStreamAsync();
        var streamReader = new StreamReader(stream);
        var content = await streamReader.ReadToEndAsync();
        return content;
    }
    
}    
