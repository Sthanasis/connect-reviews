namespace connect.Reviews.Models;

public class AppDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ReviewCollectionName { get; set; } = null!;
}