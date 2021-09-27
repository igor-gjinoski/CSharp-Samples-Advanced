using Microsoft.AspNetCore.Mvc;
using RabbitMQProducer.Messaging;
using System.Collections.Generic;

namespace RabbitMQProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> Data = new List<WeatherForecast>
        {
            new WeatherForecast { Summary = "Cold" }
        };

        private readonly IWeatherPublisher _weatherPublisher;

        public WeatherForecastController(IWeatherPublisher weatherPublisher)
            => _weatherPublisher = weatherPublisher;


        [HttpGet]
        public IActionResult Get(string weather)
        {
            if (string.IsNullOrEmpty(weather))
                return BadRequest();
            
            var fullWeather = new WeatherForecast { Summary = weather };

            Data.Add(fullWeather);
            _weatherPublisher.Publish(fullWeather);

            return this.Ok();
        }
    }
}
