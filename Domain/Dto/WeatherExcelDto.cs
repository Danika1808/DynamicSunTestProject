using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class WeatherExcelDto
    {
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public double? AirTemperature { get; set; }
        public double? RelativeHumidity { get; set; }
        public double? Td { get; set; }
        public double? AtmospherePressure { get; set; }
        public string? WindDirection { get; set; }
        public double? WindSpeed { get; set; }
        public double? Cloudy { get; set; }
        public double? CloudLowBoundary { get; set; }
        public double? HorizontalVsibility { get; set; }
        public string? WeatherEvents { get; set; }
    }
}
