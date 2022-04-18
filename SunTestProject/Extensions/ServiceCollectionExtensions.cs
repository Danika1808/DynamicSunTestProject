using AutoMapper;
using SunTestProject.Mapper;

namespace SunTestProject.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(conf => 
            {
                conf.AddProfile<WeatherProfile>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
