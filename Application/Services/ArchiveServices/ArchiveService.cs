using Application.Results;
using Application.Services.ExcelConverterService;
using Application.Services.ExcelFileReaderService;
using AutoMapper;
using Domain.Weather;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ArchiveServices
{
    public class ArchiveService : IArchiveService
    {
        private readonly AppDbContext _context;
        private readonly IExcelFileReader _excelFileReader;
        private readonly IExcelConverter _excelConverter;

        public ArchiveService(AppDbContext context, IExcelFileReader excelFileReader, IExcelConverter excelConverter)
        {
            _context = context;
            _excelFileReader = excelFileReader;
            _excelConverter = excelConverter;
        }

        public async Task<Result<int>> AddWeatherAsync(IFormFile formFile)
        {
            var readFileResult = _excelFileReader.ReadFile(formFile);

            if (readFileResult.IsSucceess)
            {
                var result = _excelConverter.ConvertToWeather(readFileResult.Value);
                
                if (result.IsSucceess)
                {
                    var isExist = result.Value.Any(x => _context.Weathers.Any(y => y.Date == x.Date && y.Time == x.Time));

                    if (isExist)
                    {
                        return Result<int>.CreateFailure(Error.Conflict, "Конфликт при добавлении в базу данных");
                    }

                    await _context.Weathers.AddRangeAsync(result.Value);

                    await _context.SaveChangesAsync();

                    return Result<int>.CreateSuccess(result.Value.Count);
                }
                return Result<int>.CreateFailure(result);
            }
            return Result<int>.CreateFailure(readFileResult);
        }
    }
}
