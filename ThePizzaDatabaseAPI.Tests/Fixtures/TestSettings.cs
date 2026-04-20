using System.Text.Json;
using Microsoft.Extensions.Configuration;

public static class TestSettings
{
    private static readonly IConfiguration Config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Test.json")
        .Build();

    public static string GetApiUrl() => Config["ApiUrl"]!;
    public static string GetApiKey() => Config["ApiKey"]!;
}