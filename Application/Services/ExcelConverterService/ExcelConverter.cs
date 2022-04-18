using Application.Results;
using AutoMapper;
using Domain.Dto;
using Domain.Weather;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExcelConverterService
{
    public class ExcelConverter : IExcelConverter
    {
        public readonly IMapper _mapper;
        public readonly string[] tableColumnName = new[] { "Дата", "Время", "Т", "Отн. влажность", "Td", "Атм. давление,", "Направление", "Скорость", "Облачность,", "h", "VV", "Погодные явления" };

        public ExcelConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Result<ICollection<Weather>> ConvertToWeather(XSSFWorkbook excelBook)
        {
            var result = new List<Weather>();

            var sheetCount = excelBook.Count;

            for (int i = 0; i < sheetCount; i++)
            {
                var sheet = excelBook.GetSheetAt(i);

                if(!IsValidSheet(sheet))
                {
                    return Result<ICollection<Weather>>.CreateFailure(Error.BadRequest, $"Страница {i+1} не валидна");
                }

                for (int row = 4; row <= sheet.LastRowNum; row++)
                {
                    var data = new string[12];

                    var currentRow = sheet.GetRow(row);

                    var weatherDto = WeatherFromRow(currentRow, data);

                    if (weatherDto.Date.HasValue && weatherDto.Time.HasValue)
                    {
                        var weather = _mapper.Map<Weather>(weatherDto);

                        result.Add(weather);
                    }
                    else
                    {
                        return Result<ICollection<Weather>>.CreateFailure(Error.BadRequest, $"Страница - {i+1}, Строка - {row + 1}. Дата или время должны иметь значение");
                    }
                }
            }

            return Result<ICollection<Weather>>.CreateSuccess(result);
        }

        private static WeatherExcelDto WeatherFromRow(IRow currentRow, string[] data)
        {
            var columnNum = 0;

            var cellValue = currentRow.GetCell(columnNum);

            while (cellValue is not null)
            {
                data[columnNum] = GetCellValue(cellValue);
                columnNum++;

                cellValue = currentRow.GetCell(columnNum);
            }
            var weatherExcelDto = CreateWeatherDto(data);

            return weatherExcelDto;
        }

        private static WeatherExcelDto CreateWeatherDto(string[] data)
        {
            return new WeatherExcelDto
            {
                Date = ParseCellToDate(data[0]),
                Time = ParseCellToTime(data[1]),
                AirTemperature = ParseCellToDouble(data[2]),
                RelativeHumidity = ParseCellToDouble(data[3]),
                Td = ParseCellToDouble(data[4]),
                AtmospherePressure = ParseCellToDouble(data[5]),
                WindDirection = data[6],
                WindSpeed = ParseCellToDouble(data[7]),
                Cloudy = ParseCellToDouble(data[8]),
                CloudLowBoundary = ParseCellToDouble(data[9]),
                HorizontalVsibility = ParseCellToDouble(data[10]),
                WeatherEvents = data[11]
            };
        }

        private static double? ParseCellToDouble(string data)
        {
            var result = double.TryParse(data, out double value);

            if (result)
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        private static DateTime? ParseCellToTime(string data)
        {
            var date = new DateTime();

            var result = DateTime.TryParse(data, out DateTime value);

            if (result)
            {
                return date.AddHours(value.Hour);
            }
            else
            {
                return null;
            }
        }

        private static DateTime? ParseCellToDate(string data)
        {
            var result = DateTime.TryParse(data, out DateTime value);

            if (result)
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        private bool IsValidSheet(ISheet sheet)
        {
            var isValid = false;
            var tableHeader = sheet.GetRow(2);

            if (tableHeader == null)
            {
                return isValid;
            }

            if (tableHeader.Count() == 12)
            {
                var temp = tableHeader.Select(x => x.StringCellValue).ToArray();
                for (int z = 0; z < temp.Length; z++)
                {
                    if (temp[z] == tableColumnName[z])
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        private static string? GetCellValue(ICell cell)
        {
            var value = cell.CellType switch
            {
                CellType.Numeric => cell.NumericCellValue.ToString(),
                CellType.String => cell.StringCellValue.Trim(),
                CellType.Boolean => cell.BooleanCellValue.ToString(),
                CellType.Error => cell.ErrorCellValue.ToString(),
                CellType.Unknown => null,
                CellType.Formula => null,
                CellType.Blank => null,
                _ => null
            };

            return value;
        }
    }
}
