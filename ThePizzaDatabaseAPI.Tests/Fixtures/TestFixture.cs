namespace ThePizzaDatabaseAPI.Tests.Fixtures;

public class TestFixture
{
    public HttpClient Client { get; }

    public TestFixture()
    {
        var baseUrl = TestSettings.GetApiUrl();

        Client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }
}