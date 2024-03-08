using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using StockRich.API.Integration.Tests.Utility;

namespace StockRich.API.Integration.Tests.Controllers;

public class RegisterTests
{
    private readonly HttpClient _httpClient;

    public RegisterTests()
    {
        var factory = new WebApplicationFactory<StockRich.API.Program>();
        _httpClient = factory.CreateClient();
    }

    [TestCase("test1", "test1", "test1", HttpStatusCode.OK)]
    [TestCase("", "", "", HttpStatusCode.BadRequest)]
    [TestCase("test1", "", "", HttpStatusCode.BadRequest)]
    [TestCase("", "test1", "", HttpStatusCode.BadRequest)]
    [TestCase("", "", "test1", HttpStatusCode.BadRequest)]
    [TestCase("t12", "test1", "test1", HttpStatusCode.BadRequest)]
    public async Task HttpPost_User_Validations_Tests(string account, string password, string name, HttpStatusCode statusCode)
    {
        var request = new
        {
            account = account,
            password = password,
            name = name
        };
        var response = await _httpClient.PostAsync("/api/Register/User", ContentHelper.WrapJsonStringContent(request));
        response.StatusCode.Should().Be(statusCode);
    }
}