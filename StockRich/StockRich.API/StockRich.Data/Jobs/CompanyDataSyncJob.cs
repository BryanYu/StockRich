using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using Hangfire.Common;
using Microsoft.Extensions.Options;
using StockRich.Domain.Config;
using StockRich.Infrastructure.Data;
using StockRich.Infrastructure.Models;

namespace StockRich.Data.Jobs;

public class CompanyDataSyncJob
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenDataUrlConfig _openDataUrlConfig;
    private readonly ILogger<CompanyDataSyncJob> _logger;
    private readonly StockRichContext _stockRichContext;

    
    public CompanyDataSyncJob(IHttpClientFactory httpClientFactory, IOptions<OpenDataUrlConfig> openDataUrlOptions, ILogger<CompanyDataSyncJob> logger, StockRichContext stockRichContext)
    {
        _httpClientFactory = httpClientFactory;
        _openDataUrlConfig = openDataUrlOptions.Value;
        _logger = logger;
        _stockRichContext = stockRichContext;
    }
    
    internal async Task Execute()
    {
        var dataContent = await FetchDataAsync();
        var newStocks  = await GetNewStocksAsync(dataContent);
        await _stockRichContext.CompanyInfos.AddRangeAsync(newStocks);
        await _stockRichContext.SaveChangesAsync();
    }

    internal async Task<IEnumerable<CompanyInfo>> GetNewStocksAsync(string dataContent)
    {
        var jsonArray = JsonArray.Parse(dataContent).AsArray();
        var stockCodes = jsonArray.Select(item => item["公司代號"].GetValue<string>()).ToList();
        var existStockCodes = _stockRichContext.CompanyInfos.Where(item => stockCodes.Contains(item.StockCode))
            .Select(item => item.StockCode);
        var newStockCodes = stockCodes.Except(existStockCodes);
        var newStocks = jsonArray.Where(item => newStockCodes.Contains(item["公司代號"].GetValue<string>())).Select(item =>
        {
            DateOnly.TryParseExact(item["成立日期"].GetValue<string>(), "yyyyMMdd", null, DateTimeStyles.None,
                out var beginDate);
            DateOnly.TryParseExact(item["上市日期"].GetValue<string>(), "yyyyMMdd", null, DateTimeStyles.None,
                out var publicDate);
            decimal.TryParse(item["實收資本額"].GetValue<string>(), out var capital);
            return new CompanyInfo
            {
                Id = Guid.NewGuid(),
                StockCode = item["公司代號"].GetValue<string>(),
                Name = item["公司名稱"].GetValue<string>(),
                Alias = item["公司簡稱"].GetValue<string>(),
                IndustryType = item["產業別"].GetValue<string>(),
                ChairMan = item["董事長"].GetValue<string>(),
                GeneralManager = item["總經理"].GetValue<string>(),
                SpokesMan = item["發言人"].GetValue<string>(),
                ContactTelephone = item["總機電話"].GetValue<string>(),
                BeginDate = beginDate,
                PublicDate = publicDate,
                Capital = capital,
                ContactEmail = item["電子郵件信箱"].GetValue<string>(),
                Website = item["網址"].GetValue<string>(),
                CreateDatetime = DateTime.Now,
                UpdateDatetime = DateTime.Now
            };
        });
        return newStocks;
    }

    
    internal async Task<string> FetchDataAsync()
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
