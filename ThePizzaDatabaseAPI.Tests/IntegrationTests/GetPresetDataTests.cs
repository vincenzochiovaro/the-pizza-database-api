using ThePizzaDatabaseAPI.Tests.Fixtures;

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
    private async Task given_when_then()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, Url);
        request.Headers.Add("X-API-Key", "publicApiKeyYesYouCanHaveIt");
        
        var foo = await _client.SendAsync(request);
        //
        // //  when calling a get preset when lang is it then assert on the title. we could hardcode the title Direct or Diretto
        // // make a theory to have both call, and assert that is not an empty array
    }
}