namespace connect.Reviews.Models;

public class ReviewMessageModel
{
    public required string ReviewId { get; set; }
    public required string ProductId { get; set; }
    public int ReviewScore { get; set; }
}