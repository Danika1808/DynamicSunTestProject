using Application.Services.WeatherServices;
using Microsoft.AspNetCore.Mvc;
using SunTestProject.Models;
using System.Diagnostics;

namespace SunTestProject.Controllers
{
    public class HomeController : Controller
    {
        public readonly IWeatherService _weatherService;
        public HomeController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index()
        {
            var years = await _weatherService.GetAllYears();
            var result = new IndexViewModel { Years = years };
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}