using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Response
{
   public class WeatherApiError
    {
        public string Field { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
