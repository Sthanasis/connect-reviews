using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace connect.Reviews.Models;

public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("productId")]
    public required string ProductId { get; set; }
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    [JsonPropertyName("score")]
    public int Score { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}