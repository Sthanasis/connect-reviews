using System.Text;
using System.Text.Json;
using connect_utilities.Models;
using RabbitMQ.Client;

namespace connect.Reviews.Services;
public class TaskPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public TaskPublisher()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "product_reviewed", type: ExchangeType.Fanout);
    }

    public void PublishMessage(ReviewMessage message)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        _channel.BasicPublish(exchange: "product_reviewed", "", basicProperties: null, body: body);
        Console.WriteLine(" [x] Sent {0}", message);
    }

    public void Close()
    {
        _channel.Close();
        _connection.Close();
    }
}