using Application.Results;
using Domain.Dto;
using Domain.Weather;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExcelConverterService
{
    public interface IExcelConverter
    {
        Result<ICollection<Weather>> ConvertToWeather(XSSFWorkbook excelBook);
    }
}
