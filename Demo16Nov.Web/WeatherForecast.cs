using System.Text.Json.Serialization;

namespace Demo16Nov.Web;

internal record WeatherForecast
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("temperatureC")]
    public int TemperatureC { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("temperatureF")]
    public int TemperatureF { get; set; }
}