using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace connect.Reviews.Models;

public class ErrorViewModel
{
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }
    [JsonPropertyName("showRequestId")]
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("status")]
    public int Status { get; set; }
}
