using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models;

namespace WebAPI
{
    public class WeatherForecast
    {
        
        [Key]
        public long WeatherForecastId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int? Temperature { get; set; } = null;

        public double? AirHumidity { get; set; } = null;
        public double? AirPressure { get; set; } = null;


        [ForeignKey("ObservedLocation")]
        public long ObservedLocationId { get; set; }
        public ObservedLocation ObservedLocation { get; set; }

    }
}
