namespace StockRich.API.Tests;

public class HttpMessageMockHandler : HttpMessageHandler
{
    private static HttpResponseMessage _response;
    
    public static void SetResponse(HttpResponseMessage responseMessage)
    {
        _response = responseMessage;
    }
    
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_response);
    }
}