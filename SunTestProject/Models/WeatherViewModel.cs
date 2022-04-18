using Domain.Dto;

namespace SunTestProject.Models
{
    public class WeatherViewModel
    {
        public ICollection<WeatherDto> Weathers { get; set; }
        public int Count { get; set; }
    }
}
