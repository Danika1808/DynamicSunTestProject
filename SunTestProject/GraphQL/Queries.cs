using Domain.Weather;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace SunTestProject.GraphQL
{
    public class Queries
    {
        public IQueryable<Weather> Weathers([FromServices] AppDbContext _appDbContext)
        {
            return _appDbContext.Weathers;
        }
    }
}
