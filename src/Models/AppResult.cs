using System.Text.Json.Serialization;

namespace connect.Reviews.Models;

public class AppResult
{
    [JsonPropertyName("error")]
    public ErrorViewModel Error { get; set; } = null!;
}

public class AppResult<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;

}