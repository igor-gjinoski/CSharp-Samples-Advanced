using MassTransit;
using System.Threading.Tasks;

namespace RabbitMQConsumer.Messages
{
    public class WeatherReceiver_MassTrasit
    {
    }
    public class UserCreatedConsumer : IConsumer<WeatherForecast>
    {
        public Task Consume(ConsumeContext<WeatherForecast> context)
        {
            var message = context.Message;

            // TODO: Do something with the data
            System.Console.WriteLine(message.Date);
            System.Console.WriteLine(message.Summary);

            return Task.CompletedTask;
        }
    }
}
