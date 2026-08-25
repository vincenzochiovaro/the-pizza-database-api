using ThePizzaDatabaseAPI.Tests.Fixtures;
using System.Net;
using System.Net.Http.Json;
using ThePizzaDatabaseAPI.Models.Responses;

namespace ThePizzaDatabaseAPI.Tests.IntegrationTests;

[Collection("IntegrationTests")]
public class GetPresetDataTests
{
    private readonly HttpClient _client;

    private const string ValidUrl =
        "GetPresetData?preset=Direct&lang=eng&doughBallCount=6&doughBallWeight=250&hydration=65&temperature=18&preferment=80";

    public GetPresetDataTests(TestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task given_valid_preset_request_when_calling_api_then_returns_valid_preset_data()
    {
        // Given
        var request = new HttpRequestMessage(HttpMethod.Get, ValidUrl);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var responseMessage = await _client.SendAsync(request);

        // Then
        responseMessage.EnsureSuccessStatusCode();

        var presetResponse = await responseMessage.Content.ReadFromJsonAsync<GetPresetResponse>();

        Assert.NotNull(presetResponse);
        Assert.NotNull(presetResponse.Tips);
        Assert.NotEmpty(presetResponse.Steps);

        Assert.NotEmpty(presetResponse.Tips.Home);
        Assert.NotEmpty(presetResponse.Tips.Professional);

        const int expectedStepCount = 6;
        Assert.Equal(expectedStepCount, presetResponse.Steps.Count);
        
        Assert.True(presetResponse.Flour > 0);
        Assert.True(presetResponse.Water > 0);
        Assert.True(presetResponse.Yeast >= 0);
    }

    [Fact]
    public async Task given_invalid_dough_ball_count_when_calling_api_then_returns_bad_request()
    {
        // Given (invalid: > 20)
        var url =
            "GetPresetData?preset=Direct&lang=eng&doughBallCount=25&doughBallWeight=250&hydration=65&temperature=18";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var responseMessage = await _client.SendAsync(request);

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);
    }

    [Fact]
    public async Task given_missing_preferment_when_calling_api_then_returns_valid_response()
    {
        // Given (preferment is optional)
        var url =
            "GetPresetData?preset=Direct&lang=eng&doughBallCount=6&doughBallWeight=250&hydration=65&temperature=18";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var responseMessage = await _client.SendAsync(request);

        // Then
        responseMessage.EnsureSuccessStatusCode();

        var presetResponse = await responseMessage.Content.ReadFromJsonAsync<GetPresetResponse>();

        Assert.NotNull(presetResponse);
    }

    [Fact]
    public async Task given_biga_preset_when_calling_api_then_returns_day2_values()
    {
        // Given
        var url =
            "GetPresetData?preset=Biga&lang=eng&doughBallCount=6&doughBallWeight=250&hydration=65&temperature=18&preferment=50";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-API-Key", TestSettings.GetApiKey());

        // When
        var responseMessage = await _client.SendAsync(request);

        // Then
        responseMessage.EnsureSuccessStatusCode();

        var presetResponse = await responseMessage.Content.ReadFromJsonAsync<GetPresetResponse>();

        Assert.NotNull(presetResponse);
        
        Assert.NotNull(presetResponse.WaterDay2);
        Assert.NotNull(presetResponse.FlourDay2);
        Assert.NotNull(presetResponse.SaltDay2);
    }
}