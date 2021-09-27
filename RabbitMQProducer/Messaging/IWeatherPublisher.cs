namespace RabbitMQProducer.Messaging
{
    public interface IWeatherPublisher
    {
        void Publish(WeatherForecast weather);
    }
}
