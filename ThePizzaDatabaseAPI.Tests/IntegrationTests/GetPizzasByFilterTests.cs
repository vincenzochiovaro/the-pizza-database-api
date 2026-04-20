using ThePizzaDatabaseAPI.Tests.Fixtures;
using System.Net;

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
    public async Task given_valid_filter_when_calling_api_then_returns_ok_and_data()
    {
        // Given
        var url = $"{BaseUrl}?filter=AllPizzas&lang=eng";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var response = await _client.SendAsync(request);

        // Then
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        Assert.False(string.IsNullOrEmpty(content));
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

        var content = await response.Content.ReadAsStringAsync();

        // simple check: we still get something back
        Assert.False(string.IsNullOrEmpty(content));
    }
}