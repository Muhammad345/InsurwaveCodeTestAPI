using Data.Constant;
using InsurwaveCodeTestAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace InsurwaveCodeTestAPI_IntegrationTests
{
    public class WeatherInfoControllerIntegrationTests
    {
        private readonly WeatherInfoController WeatherInfoController;
        private string _city;
        private string _tempMeasurementUnit;
        private DateTime? date;


        [SetUp]
        public void Setup()
        {
            _city = "London";
            _tempMeasurementUnit = InsurwaveConstants.TemperatureMeasurement.Celsius;
            date = DateTime.UtcNow;
            //WeatherInfoController = new WeatherInfoController()
        }

        [Test]
        public async Task Get_CityWeatherInfo_Successfull()
        {
            var actionResult = await WeatherInfoController.Get(_city, date);

            var okOjbectResult = actionResult as OkObjectResult;
            var localCityWeatherInfo = okOjbectResult.Value;

            Assert.IsNotNull(okOjbectResult);

        }
    }
}