using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public sealed class TaskConsumer : BackgroundService
{
    private IConnection? connection;
    private IModel? channel;
    private void StartListeningForProductUpdated()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "product_updated", type: ExchangeType.Fanout);
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName,
                          exchange: "product_updated",
                          routingKey: "");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await Task.CompletedTask;
            Console.WriteLine(" [x] Received product_updated {0}", message);
        };
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine(" [x] Listening for product_updated");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {

            StartListeningForProductUpdated();
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
        }
        finally
        {

            await Task.CompletedTask;
        }
    }

}