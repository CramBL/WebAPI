using System;

namespace WebAPI
{
    public class WeatherForecast
    {
        private static WeatherForecast instance = null;

        public DateTime Date { get; set; }

        public int? Temperature { get; set; } = null;

        public double? AirHumidity { get; set; } = null;
        public double? AirPressure { get; set; } = null;

        public string Summary { get; set; } = null;
    }
}
