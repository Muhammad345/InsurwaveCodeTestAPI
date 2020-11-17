using Data.Constant;
using InsurwaveCodeTestAPI;
using InsurwaveCodeTestAPI.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InsurwaveCodeTestAPI_IntegrationTests
{
    public class WeatherInfoControllerIntegrationTests
    {
        private readonly WeatherInfoController WeatherInfoController;
        private string _city;
        private string _tempMeasurementUnit;
        private DateTime? date;

        private readonly TestServer _server;
        private readonly HttpClient _client;
        public WeatherInfoControllerIntegrationTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

            _city = "London";
            _tempMeasurementUnit = InsurwaveConstants.TemperatureMeasurement.Celsius;
            date = DateTime.UtcNow;
        }

        [Test]
        public async Task Get_CityWeatherInfo_Successfull()
        {
            // Act
            var response = await _client.GetAsync("/WeatherInfo/london");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
           
        }
    }
}