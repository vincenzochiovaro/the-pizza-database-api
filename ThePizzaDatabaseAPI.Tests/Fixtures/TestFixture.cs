using System.Diagnostics;

namespace ThePizzaDatabaseAPI.Tests.Fixtures;

public class TestFixture : IDisposable
{
    private readonly Process _process;
    public HttpClient Client { get; }

    public TestFixture()
    {
        var baseUrl = TestSettings.GetApiUrl();

        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "func",
                Arguments = "host start --port 7032",
                WorkingDirectory = Path.GetFullPath(
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "../../../../ThePizzaDatabaseAPI"
                    )
                ),
                UseShellExecute = false
            }
        };

        _process.Start();

        using var client = new HttpClient();
        for (var i = 0; i < 60; i++)
        {
            try
            {
                client.GetAsync(baseUrl).GetAwaiter().GetResult();
                break;
            }
            catch (HttpRequestException)
            {
                Thread.Sleep(500);
            }
        }

        Client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public void Dispose()
    {
        Client.Dispose();
        if (!_process.HasExited)
        {
            _process.Kill(true);
            _process.WaitForExit();
        }
        _process.Dispose();
    }
}