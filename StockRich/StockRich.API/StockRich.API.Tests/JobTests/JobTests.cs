using System.Net;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Options;
using NSubstitute;
using StockRich.Data.Jobs;
using StockRich.Domain.Config;

namespace StockRich.API.Tests.JobTests;

public class JobTests
{
    private HttpClient _fakeHttpClient;
    private IHttpClientFactory _httpClientFactory;
    private ILogger<CompanyDataSyncJob> _logger;
    private IOptions<OpenDataUrlConfig> _options;
    
    
    public JobTests()
    {
        var httpClientFactory = NSubstitute.Substitute.For<IHttpClientFactory>();
        _options = Options.Create(new OpenDataUrlConfig { TaiwanStockExchange = "http://example.com" });
        _logger = NSubstitute.Substitute.For<ILogger<CompanyDataSyncJob>>();
        var handlerMock = new HttpMessageMockHandler();
        _fakeHttpClient = new HttpClient(handlerMock);
        httpClientFactory.CreateClient().Returns(_fakeHttpClient);
    }

    [Test]
    public async Task CompanyDataSyncJobTests()
    {
        var arrange = new CompanyDataSyncJob(_httpClientFactory, _options, _logger);
        var fakeMemoryStream = new MemoryStream();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(fakeMemoryStream)
        };
        HttpMessageMockHandler.SetResponse(responseMessage);
        await arrange.Execute();
        _logger.DidNotReceiveWithAnyArgs().LogError(default);
    }
}
