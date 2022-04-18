using AutoMapper;
using Domain.Dto;
using Domain.Weather;

namespace SunTestProject.Mapper
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherExcelDto, Weather>();
            CreateMap<Weather, WeatherDto>()
                .ForMember("Date", options => options.MapFrom(x => x.Date.ToShortDateString()))
                .ForMember("Time", options => options.MapFrom(x => x.Time.ToShortTimeString()));
        }
    }
}
