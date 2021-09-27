using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQCommon;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer.Messaging
{
    public class WeatherReceiver : BackgroundService
    {
        private readonly RabbitMqConfiguration _rabbitMq;
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public WeatherReceiver(IOptions<RabbitMqConfiguration> rabbitMq)
        {
            _rabbitMq = rabbitMq.Value;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMq.Hostname,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_rabbitMq.QueueName, exclusive: false, autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var weather = JsonSerializer.Deserialize<WeatherForecast>(ea.Body.ToArray());

                // TODO: Do something with the data
                System.Console.WriteLine(weather.Date);
                System.Console.WriteLine(weather.Summary);
                
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_rabbitMq.QueueName, false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
            base.Dispose();
        }
    }
}
