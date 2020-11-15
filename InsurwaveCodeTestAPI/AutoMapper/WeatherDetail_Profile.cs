using AutoMapper;
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
            CreateMap<Category, Create_CategoryViewModel>()
              .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.CurrentDateTimeUtc, opt => opt.MapFrom(src => src.CreatedByDateTime))
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CreatedBy))
              .ReverseMap();
        }
    }
}
