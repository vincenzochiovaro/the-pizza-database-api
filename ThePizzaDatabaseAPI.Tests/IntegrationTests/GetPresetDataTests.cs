using ThePizzaDatabaseAPI.Tests.Fixtures;
using System.Net.Http.Json;
using ThePizzaDatabaseAPI.Models.Responses;

namespace ThePizzaDatabaseAPI.Tests.IntegrationTests;

public class GetPresetDataTests : IClassFixture<TestFixture>
{
    private readonly HttpClient _client;

    private const string Url =
        "?preset=Direct&lang=eng&doughBallCount=6&doughBallWeight=250&hydration=65&temperature=18&preferment=80";

    public GetPresetDataTests(TestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    private async Task given_valid_preset_request_when_calling_api_then_returns_valid_preset_data()
    {
        // Given
        var request = new HttpRequestMessage(HttpMethod.Get, Url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var responseMessage = await _client.SendAsync(request);
        responseMessage.EnsureSuccessStatusCode();

        var presetResponse = await responseMessage.Content.ReadFromJsonAsync<GetPresetResponse>();

        // Then
        Assert.NotNull(presetResponse);
        Assert.NotEmpty(presetResponse.Steps);

        const int expectedStepCount = 5;
        Assert.Equal(expectedStepCount, presetResponse.Steps.Count);
    }
}