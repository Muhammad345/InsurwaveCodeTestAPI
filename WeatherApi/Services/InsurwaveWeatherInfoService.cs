using Data;
using Data.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi.Services
{
    public class InsurwaveWeatherInfoService : IInsurwaveWeatherInfoService
    {
        private string _queryString = "&q=";
        
        private readonly WeatherApiConfiguration _weatherApiConfiguration;
        static readonly HttpClient client = new HttpClient();

        public InsurwaveWeatherInfoService(IOptions<WeatherApiConfiguration> options)
        {
            _weatherApiConfiguration = options.Value;
        }
        public async Task<WeatherApiHttpClientResponse> GetLocalWeatherInfo(string cityName)
        {
            var response = new HttpResponseMessage();

            try
            {
                response =await client.GetAsync(GetUrl() + _queryString + cityName);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var weatherApiHttpClientResponse = new WeatherApiHttpClientResponse {
                     Data = responseBody,
                     IsSuccessFull = response.IsSuccessStatusCode,
                     StatusCode = response.StatusCode
                };

                return weatherApiHttpClientResponse;
            }
            catch (HttpRequestException e)
            {
                return new WeatherApiHttpClientResponse
                {
                    Data = e.Message,
                    IsSuccessFull = false,
                    Exception = e,
                    StatusCode = response.StatusCode
                };
            }

        }


        private string GetUrl()
        {
            return $"{_weatherApiConfiguration.Url}{_weatherApiConfiguration.ApiKey}";
        }
    }
}
