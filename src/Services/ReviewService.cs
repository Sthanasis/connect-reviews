using connect.Reviews.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace connect.Reviews.Services;

public class ReviewService
{
    private readonly IMongoCollection<Review> _reviewCollection;
    public ReviewService(
        IOptions<AppDatabaseSettings> appDatabaseSettings)
    {
        var settings = MongoClientSettings.FromConnectionString(appDatabaseSettings.Value.ConnectionString);
        // Request timeout
        settings.ServerSelectionTimeout = TimeSpan.FromSeconds(10);
        var mongoClient = new MongoClient(settings);

        var mongoDatabase = mongoClient.GetDatabase(
            appDatabaseSettings.Value.DatabaseName);

        _reviewCollection = mongoDatabase.GetCollection<Review>(
            appDatabaseSettings.Value.ReviewCollectionName);
    }

    public async Task<List<Review>> GetByProductId(string productId) => await _reviewCollection.Find(r => r.ProductId == productId).ToListAsync();
    public async Task<List<Review>> GetByUserId(int userId) => await _reviewCollection.Find(r => r.UserId == userId).ToListAsync();

    public async Task<Review?> GetAsync(string id) =>
        await _reviewCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Review newReview)
    {
        await _reviewCollection.InsertOneAsync(newReview);
        TaskPublisher _publisher = new();
        _publisher.PublishMessage(new ReviewMessageModel { ReviewId = newReview.Id!, ReviewScore = newReview.Score, ProductId = newReview.ProductId });
        _publisher.Close();
    }


    public async Task RemoveAsync(string id) =>
        await _reviewCollection.DeleteOneAsync(x => x.Id == id);
}