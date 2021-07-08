using System.Threading.Tasks;

namespace SignalRCommon
{
    public interface IWeatherClient
    {
        Task ReceiveError(string error);

        Task ReceiveData(WeatherForecast data);
    }
}
