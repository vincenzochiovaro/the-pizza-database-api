using System.Text.Json;

public static class TestSettings
{
    public static string GetApiKey()
    {
        var json = File.ReadAllText("local.settings.json");
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("Values").GetProperty("PIZZA_DB_API_KEY").GetString();
    }
}