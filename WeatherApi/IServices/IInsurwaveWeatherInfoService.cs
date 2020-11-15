using Data.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi
{
    public interface IInsurwaveWeatherInfoService
    {
        Task<WeatherApiHttpClientResponse> GetLocalWeatherInfo(string cityName);
    }
}
