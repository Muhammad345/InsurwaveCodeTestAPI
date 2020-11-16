using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CityWeatherDetail;
using Data.Constant;
using Data.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherApi;

namespace InsurwaveCodeTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherInfoController : ControllerBase
    {

        private readonly ILogger<WeatherInfoController> _logger;
        private readonly IInsurwaveWeatherInfoService _insurwaveWeatherInfoService;
        private readonly IMapper _mapper;

        public WeatherInfoController(ILogger<WeatherInfoController> logger, IInsurwaveWeatherInfoService insurwaveWeatherInfoService, IMapper mapper)
        {
            _logger = logger;
            _insurwaveWeatherInfoService = insurwaveWeatherInfoService;
            _mapper = mapper;
        }

        [HttpGet("{city}/{date:datetime?}")]
        public async Task<IActionResult> Get(string city, DateTime? date = null)
        {
            try
            {
                var weatherApiHttpClientResponse = await _insurwaveWeatherInfoService.GetLocalWeatherInfo(city);
                var astronomyResponse = await _insurwaveWeatherInfoService.GetAstronomyInfo(city, date);

                if (astronomyResponse.IsSuccessFull && weatherApiHttpClientResponse.IsSuccessFull)
                {
                    var weatherInformation = JsonConvert.DeserializeObject<CurrentDetail>(weatherApiHttpClientResponse.Data);
                    var localWeatherDetail = _mapper.Map<CityWeatherInfo>(weatherInformation);
                    var astronomyDetail = JsonConvert.DeserializeObject<AstronomyDetail>(astronomyResponse.Data);
                    localWeatherDetail.Sunrise = astronomyDetail.Astronomy.Astro.Sunrise;
                    localWeatherDetail.Sunset = astronomyDetail.Astronomy.Astro.Sunset;

                    return Ok(localWeatherDetail);

                }
            }
            catch (Exception exp)
            {
                _logger.LogError("Error During Get weather info Call", exp);
                return BadRequest(exp);
            }


            return NoContent();
        }


        [HttpGet("{city}/{tempMeasurementUnit}/{date:datetime?}")]
        public async Task<IActionResult> Get(string city,string tempMeasurementUnit, DateTime? date = null)
        {
            try
            {
                if(!tempMeasurementUnit.Equals(InsurwaveConstants.TemperatureMeasurement.Celsius,StringComparison.InvariantCultureIgnoreCase) && !tempMeasurementUnit.Equals(InsurwaveConstants.TemperatureMeasurement.Fahrenheit,StringComparison.InvariantCultureIgnoreCase))
                {
                    return BadRequest();
                }

                var weatherApiHttpClientResponse = await _insurwaveWeatherInfoService.GetLocalWeatherInfo(city);
                var astronomyResponse = await _insurwaveWeatherInfoService.GetAstronomyInfo(city, date);

                if (astronomyResponse.IsSuccessFull && weatherApiHttpClientResponse.IsSuccessFull)
                {
                    var weatherInformation = JsonConvert.DeserializeObject<CurrentDetail>(weatherApiHttpClientResponse.Data);
                    var localWeatherDetail = _mapper.Map<CityDetailWithTempUnit>(weatherInformation);
                    var astronomyDetail = JsonConvert.DeserializeObject<AstronomyDetail>(astronomyResponse.Data);
                    localWeatherDetail.Sunrise = astronomyDetail.Astronomy.Astro.Sunrise;
                    localWeatherDetail.Sunset = astronomyDetail.Astronomy.Astro.Sunset;

                    if (tempMeasurementUnit == InsurwaveConstants.TemperatureMeasurement.Celsius)
                    {
                        localWeatherDetail.TempMeasurementInUnit = InsurwaveConstants.TemperatureMeasurement.Celsius;
                        localWeatherDetail.Temperature = weatherInformation.Current.TempC;
                        return Ok(localWeatherDetail);
                    }

                    if (tempMeasurementUnit == InsurwaveConstants.TemperatureMeasurement.Fahrenheit)
                    {
                        localWeatherDetail.TempMeasurementInUnit = InsurwaveConstants.TemperatureMeasurement.Fahrenheit;
                        localWeatherDetail.Temperature = weatherInformation.Current.TempF;
                        return Ok(localWeatherDetail);
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError("Error During Get weather info Call", exp);
                return BadRequest(exp);
            }


            return NoContent();
        }
    }
}
