using CityWeatherDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Response
{
    public class CityDetailWithTempUnit : CityWeatherInfo
    {
        public string TempMeasurementInUnit { get; set; }

    }
}
