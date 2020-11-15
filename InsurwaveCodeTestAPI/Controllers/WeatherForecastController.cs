using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityWeatherDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApi;

namespace InsurwaveCodeTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IInsurwaveWeatherInfoService _insurwaveWeatherInfoService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IInsurwaveWeatherInfoService insurwaveWeatherInfoService)
        {
            _logger = logger;
            _insurwaveWeatherInfoService = insurwaveWeatherInfoService;
        }

        [HttpGet]
        public IEnumerable<CityWeatherDetailResponse> Get(string cityName)
        {
            var weatherApiHttpClientResponse = _insurwaveWeatherInfoService.GetLocalWeatherInfo(cityName);

            return null;
        }
    }
}
