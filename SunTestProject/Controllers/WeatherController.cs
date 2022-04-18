using Application.Services.WeatherServices;
using Domain.Dto;
using Domain.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunTestProject.Models;

namespace SunTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        public readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("getWeather/{pageNum}")]
        public async Task<ActionResult<WeatherViewModel>> GetWeather(int pageNum = 0, int year = 0, int month = 0)
        {
            var weathers = await _weatherService.GetWeathers(pageNum, year, month);

            var count = await _weatherService.Count(year, month);

            var result = new WeatherViewModel { Count = count, Weathers = weathers };

            return Ok(result);
        }

    }
}
