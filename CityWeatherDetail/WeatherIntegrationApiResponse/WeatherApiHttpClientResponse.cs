using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Data.Response
{
    public class WeatherApiHttpClientResponse
    {
        public bool IsSuccessFull { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Data { get; set; }
        public Exception Exception { get; set; }   
    }
}
