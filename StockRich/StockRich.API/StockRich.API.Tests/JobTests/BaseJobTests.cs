using Microsoft.Extensions.Options;
using NSubstitute;
using StockRich.Data.Jobs;
using StockRich.Domain.Config;

namespace StockRich.API.Tests.JobTests;

public class BaseJobTests
{
    protected HttpClient _fakeHttpClient;
    protected IHttpClientFactory _httpClientFactory;
    protected IOptions<OpenDataUrlConfig> _options;
    
    protected BaseJobTests()
    {
        this._httpClientFactory = NSubstitute.Substitute.For<IHttpClientFactory>();
        var handlerMock = new HttpMessageMockHandler();
        _fakeHttpClient = new HttpClient(handlerMock);
        this._httpClientFactory.CreateClient().Returns(_fakeHttpClient);
        _options = Options.Create(new OpenDataUrlConfig { TaiwanStockExchange = "http://example.com" });
    }
}