using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace connect.Reviews.Services;
public class TaskConsumer
{
    public void StartListening()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "product_updated", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };
        channel.BasicConsume(queue: "product_updated", autoAck: true, consumer: consumer);
    }
}