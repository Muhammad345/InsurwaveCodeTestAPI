using System;

namespace CityWeatherDetail
{
    public class CityWeatherDetailResponse
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string LocalTime { get; set; }
        public string Temperature { get; set; }
    }
}
