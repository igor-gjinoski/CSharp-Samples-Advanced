using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQCommon;
using RabbitMQ.Client;

namespace RabbitMQProducer.Messaging
{
    public class WeatherPublisher : IWeatherPublisher
    {
        private readonly RabbitMqConfiguration _rabbitMq;

        public WeatherPublisher(IOptions<RabbitMqConfiguration> rabbitMq)
        {
            _rabbitMq = rabbitMq.Value;
        }

        public void Publish(WeatherForecast weather)
        {
            var body = JsonSerializer.SerializeToUtf8Bytes(weather);

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMq.Hostname
            };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(_rabbitMq.QueueName, exclusive: false, autoDelete: false);

            channel.BasicPublish(string.Empty, _rabbitMq.QueueName, null, body);
        }
    }
}
