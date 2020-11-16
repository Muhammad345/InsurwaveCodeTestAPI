using AutoMapper;
using CityWeatherDetail;
using Data.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurwaveCodeTestAPI.AutoMapper
{
    public class WeatherDetail_Profile: Profile
    {
        public WeatherDetail_Profile()
        {
            CreateMap<CityWeatherInfo, CurrentDetail>()
              .ForPath(dest => dest.Location.Name, opt => opt.MapFrom(src => src.City))
              .ForPath(dest => dest.Location.Region, opt => opt.MapFrom(src => src.Region))
              .ForPath(dest => dest.Location.Country, opt => opt.MapFrom(src => src.Country))
              .ForPath(dest => dest.Location.Localtime, opt => opt.MapFrom(src => src.LocalTime))
              .ForPath(dest => dest.Current.TempC, opt => opt.MapFrom(src => src.Temperature))
              .ReverseMap();


            CreateMap<CityDetailWithTempUnit, CurrentDetail>()
            .ForPath(dest => dest.Location.Name, opt => opt.MapFrom(src => src.City))
            .ForPath(dest => dest.Location.Region, opt => opt.MapFrom(src => src.Region))
            .ForPath(dest => dest.Location.Country, opt => opt.MapFrom(src => src.Country))
            .ForPath(dest => dest.Location.Localtime, opt => opt.MapFrom(src => src.LocalTime))
            .ReverseMap();
        }
    }
}
