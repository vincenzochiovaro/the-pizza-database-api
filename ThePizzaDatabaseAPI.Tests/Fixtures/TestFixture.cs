namespace ThePizzaDatabaseAPI.Tests.Fixtures;

public class TestFixture
{
    public HttpClient Client { get; }

    public TestFixture()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri(TestSettings.GetApiUrl()); 
    }
}