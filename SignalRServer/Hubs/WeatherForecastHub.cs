﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRCommon;

namespace SignalRServer.Hubs
{
    public class WeatherForecastHub : Hub<IWeatherClient>, IWeatherServer
    {
        private static readonly List<WeatherForecast> Data = new List<WeatherForecast>
        {
            new WeatherForecast { Summary = "Cold" }
        };

        public WeatherForecast GetWeather()
        {
            var weather = Data.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            return weather;
        }

        public async Task SaveWeather(WeatherForecast weather)
        {
            if (string.IsNullOrEmpty(weather.Summary))
            {
                await Clients.Caller.ReceiveError("Invalid weather");
                return;
            }

            Data.Add(weather);

            var result = Data.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            await Clients.Caller.ReceiveData(result);
        }
    }
}
