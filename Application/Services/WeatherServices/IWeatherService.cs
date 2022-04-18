using Application.Results;
using Domain.Dto;
using Domain.Weather;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WeatherServices
{
    public interface IWeatherService
    {
        Task<ICollection<WeatherDto>> GetWeathers(int pageNum, int year, int month);
        Task<int> Count(int year, int month);
        Task<ICollection<int>> GetAllYears();
    }
}
