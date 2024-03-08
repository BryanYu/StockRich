using System.Text;
using System.Text.Json;

namespace StockRich.API.Integration.Tests.Utility;

public static class ContentHelper
{
    public static StringContent WrapJsonStringContent(object argument)
    {
        return new StringContent(JsonSerializer.Serialize(argument), Encoding.UTF8, "application/json");
    }
    
}