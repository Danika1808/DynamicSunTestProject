using Application.Results;
using Domain.Weather;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ArchiveServices
{
    public interface IArchiveService
    {
        Task<Result<int>> AddWeatherAsync(IFormFile formFile);
    }
}
