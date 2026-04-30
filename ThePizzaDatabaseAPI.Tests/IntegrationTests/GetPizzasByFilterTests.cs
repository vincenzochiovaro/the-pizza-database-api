using ThePizzaDatabaseAPI.Tests.Fixtures;
using System.Net;
using System.Net.Http.Json;
using ThePizzaDatabaseAPI.Models.Responses;

namespace ThePizzaDatabaseAPI.Tests.IntegrationTests;

public class GetPizzasByFilterTests : IClassFixture<TestFixture>
{
    private readonly HttpClient _client;
    private const string BaseUrl = "GetPizzasByFilter";

    public GetPizzasByFilterTests(TestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task given_missing_filter_when_calling_api_then_returns_bad_request()
    {
        // Given
        var url = $"{BaseUrl}?lang=eng";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var response = await _client.SendAsync(request);

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task given_invalid_filter_when_calling_api_then_fallback_to_all_pizzas()
    {
        // Given
        var url = $"{BaseUrl}?filter=invalidFilter&lang=eng";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var response = await _client.SendAsync(request);

        // Then
        response.EnsureSuccessStatusCode();
        
        var pizzas = await response.Content.ReadFromJsonAsync<List<GetPizzasByFilterResponse>>();

        Assert.NotNull(pizzas);
        Assert.NotEmpty(pizzas);
    }
    
    [Theory]
    [InlineData("AllPizzas")]
    [InlineData("VegetarianPizzas")]
    [InlineData("WhitePizzas")]
    // [InlineData("ClassicPizzas")] todo
    public async Task given_each_filter_when_calling_api_then_returns_expected_pizzas(string filter)
    {
        // Given
        var url = $"{BaseUrl}?filter={filter}&lang=eng";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var response = await _client.SendAsync(request);

        // Then
        response.EnsureSuccessStatusCode();

        var pizzas = await response.Content.ReadFromJsonAsync<List<GetPizzasByFilterResponse>>();

        Assert.NotNull(pizzas);
        Assert.NotEmpty(pizzas);

        if (filter == "VegetarianPizzas")
        {
            Assert.All(pizzas, p => Assert.True(p.IsVegetarian));
        }

        if (filter == "WhitePizzas")
        {
            Assert.All(pizzas, p => Assert.True(p.IsWhite));
        }

        // if (filter == "ClassicPizzas")
        // {
        //     Assert.All(pizzas, p => Assert.True(p.IsClassic));
        // }
    }
}