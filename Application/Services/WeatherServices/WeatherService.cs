using Application.Results;
using Application.Services.ExcelConverterService;
using Application.Services.ExcelFileReaderService;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Dto;
using Domain.Weather;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Services.WeatherServices
{
    public class WeatherService : IWeatherService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WeatherService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<WeatherDto>> GetWeathers(int pageNum, int year, int month)
        {
            var query = _context.Weathers.AsNoTracking().AsQueryable();

            if (year != 0)
            {
                query = query.Where(x => x.Date.Year == year);
            }

            if (month != 0)
            {
                query = query.Where(x => x.Date.Month == month);
            }

            var count = query.Count();

            var result = await query.Skip(10 * pageNum).Take(10).ProjectTo<WeatherDto>(_mapper.ConfigurationProvider).ToListAsync();

            return result;
        }

        public async Task<int> Count(int year, int month)
        {
            var query = _context.Weathers.AsNoTracking().AsQueryable();

            if (year != 0)
            {
                query = query.Where(x => x.Date.Year == year);
            }

            if (month != 0)
            {
                query = query.Where(x => x.Date.Month == month);
            }

            return await query.CountAsync();
        }

        public async Task<ICollection<int>> GetAllYears()
        {
            return await _context.Weathers.Select(x => x.Date.Year).Distinct().OrderBy(x => x).ToListAsync();
        }
    }
}
