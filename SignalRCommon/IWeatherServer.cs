using System.Threading.Tasks;

namespace SignalRCommon
{
    public interface IWeatherServer
    {
        WeatherForecast GetWeather();

        Task SaveWeather(WeatherForecast weather);
    }
}
