using MassTransit;
using System.Threading.Tasks;

namespace RabbitMQConsumer.Messages
{
    public class WeatherReceiver_MassTrasit : IConsumer<WeatherForecast>
    {
        public Task Consume(ConsumeContext<WeatherForecast> context)
        {
            var message = context.Message;

            // TODO: Do something with the data
            System.Console.WriteLine(nameof(WeatherReceiver_MassTrasit));

            return Task.CompletedTask;
        }
    }
}
