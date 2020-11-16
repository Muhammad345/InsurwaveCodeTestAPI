using AutoMapper;
using CityWeatherDetail;
using Data.Response;
using InsurwaveCodeTestAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using WeatherApi;

namespace InsurwaveCodeTestAPI_UnitTests
{
    public class WeatherInfoControllerUnitTests
    {
        private Mock<ILogger<WeatherInfoController>> _moockLogger;
        private Mock<IInsurwaveWeatherInfoService> _mockinsurwaveWeatherInfoService;
        private Mock<IMapper> _mockmapper;
        private  string _city;
        private DateTime? _date;
        private WeatherApiHttpClientResponse WeatherApiHttpClientResponse;
        private WeatherApiHttpClientResponse AstronomyWeatherApiHttpClientResponse;
        private AstronomyDetail _astronomyDetail;
        private WeatherInfoController SUT;
        private CurrentDetail _currentDetail;
        private CityWeatherInfo _cityWeatherInfo;
        


        [SetUp]
        public void Setup()
        {
            _moockLogger = new Mock<ILogger<WeatherInfoController>>();
            _mockinsurwaveWeatherInfoService = new Mock<IInsurwaveWeatherInfoService>();
            _mockmapper = new Mock<IMapper>();

            _currentDetail = new CurrentDetail
            {
                 Current = new Current
                 {
                     TempC = "11.0",
                     TempF = "54.3"
                 },
                  Location = new Location { 
                   Name = "London",
                   Country = "United Kingdom",
                   Localtime = "2020-11-16 13:02",
                   Region  = "City of London, Greater London"
                  }
            };

            _astronomyDetail = new AstronomyDetail {
                 Astronomy = new Astronomy
                 {
                     Astro = new Astro {  
                         Sunrise = "07:21 AM",
                         Sunset ="04:09 PM"
                     }
                 },
                Location = new Location
                {
                    Country = "United Kingdom",
                    Localtime = "2020-11-16 13:02",
                    Region = "City of London, Greater London"
                }
            };

            _city = "London";
            _date = DateTime.UtcNow;

            _cityWeatherInfo = new CityWeatherInfo
            { 
                City  = _currentDetail.Location.Name, 
                Country = _currentDetail.Location.Country,
                LocalTime = _currentDetail.Location.Localtime,
                Region = _currentDetail.Location.Region,
                Temperature = _currentDetail.Current.TempC
            };

            WeatherApiHttpClientResponse = new WeatherApiHttpClientResponse
            {
            Data = JsonConvert.SerializeObject(_currentDetail),
            IsSuccessFull = true,
            StatusCode = System.Net.HttpStatusCode.OK
            };

            AstronomyWeatherApiHttpClientResponse = new WeatherApiHttpClientResponse
            {
                Data = JsonConvert.SerializeObject(_astronomyDetail),
                IsSuccessFull = true,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            _mockinsurwaveWeatherInfoService.Setup(x => x.GetLocalWeatherInfo(_city)).Returns(Task.FromResult(WeatherApiHttpClientResponse));

            _mockinsurwaveWeatherInfoService.Setup(x => x.GetAstronomyInfo(_city, _date)).Returns(Task.FromResult(AstronomyWeatherApiHttpClientResponse));
          //  _moockLogger.Setup(x => x.LogError(It.IsAny<string>(), It.IsAny<Object>()));
            _mockmapper.Setup(x => x.Map<CityWeatherInfo>(It.IsAny<CurrentDetail>())).Returns(_cityWeatherInfo);

            SUT = new WeatherInfoController(_moockLogger.Object, _mockinsurwaveWeatherInfoService.Object, _mockmapper.Object);

        }

        [Test]
        public async Task GetCurrentWeatherInfo_Successfull()
        {
            //Arrange 
            _city = "London";

            //Act
            var result = await SUT.Get(_city, _date);
            var okObjectResult = result as OkObjectResult;



            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(okObjectResult.StatusCode,HttpStatusCode.OK);
           
        }
    }
}
