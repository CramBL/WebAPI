using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private WeatherData _weatherData;
        

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _weatherData = WeatherData.GetInstance();
            //_weatherData.AddWeatherForecast(new WeatherForecast()
            //{
            //    Date = DateTime.Now,
            //    Temperature = 69,
            //    AirHumidity =1337,
            //    AirPressure = 10
            //});
        }
        /// <summary>
        /// get the weather!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                Temperature = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetAllWeatherForecastList")]
        public ActionResult<List<WeatherForecast>> GetAllWeatherForecastList()
        {
            return _weatherData.getAllWeatherForecasts();
        }

        /// <summary>
        /// Posts weatherforecast data
        /// </summary>
        /// <param name="weatherData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<WeatherForecast> Post(WeatherForecast weatherData)
        {
            if (weatherData == null)
            {
                return BadRequest();
            }

            var newWeatherForecastIndex = _weatherData.AddWeatherForecast(new WeatherForecast()
            {
                Date = weatherData.Date,
                Temperature = weatherData.Temperature,
                AirHumidity = weatherData.AirHumidity,
                AirPressure = weatherData.AirPressure
            });
            return CreatedAtAction("Get", newWeatherForecastIndex,  _weatherData.ReturnLatestWeatherForecast());
        }

        /// <summary>
        /// Get latest weatherforecast
        /// </summary>
        /// <returns>
        /// Latest weatherforecast
        /// </returns>
        [HttpGet("GetLatest")]
        public ActionResult<WeatherForecast> GetLatest()
        {
            return _weatherData.ReturnLatestWeatherForecast();
        }

        [HttpGet("{date}", Name = "GetAtDate")]
        public ActionResult<List<WeatherForecast>> GetAtDate(DateTime date)
        {
            return _weatherData.GetWeatherForecastAtDate(date);
        }


    }
}
