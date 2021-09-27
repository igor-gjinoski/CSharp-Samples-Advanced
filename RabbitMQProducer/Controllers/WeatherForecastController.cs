using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQProducer.Messaging;

namespace RabbitMQProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBus _massTransitPublisher;
        private readonly IWeatherPublisher _rabbitMqPublisher;

        public WeatherForecastController(
            IWeatherPublisher weatherPublisher,
            IBus massTransitPublisher)
        {
            _rabbitMqPublisher = weatherPublisher;
            _massTransitPublisher = massTransitPublisher;
        } 


        [HttpGet]
        public IActionResult Get(string weather)
        {
            if (string.IsNullOrEmpty(weather))
                return BadRequest();

            WeatherForecast fullWeather = 
                new() 
                { 
                    Summary = weather 
                };

            _rabbitMqPublisher.Publish(fullWeather);
            _massTransitPublisher.Publish(fullWeather);

            return Ok();
        }
    }
}
