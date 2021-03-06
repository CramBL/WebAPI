using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChat> _chatHubContext;
        private ApplicationDbContext _context;

        public WeatherForecastController(
            ApplicationDbContext dbContext,
            IHubContext<ChatHub, IChat> chatHubContext)
        {
            _context = dbContext;
            _chatHubContext = chatHubContext;

            //_weatherData.AddWeatherForecast(new WeatherForecast()
            //{
            //    Date = DateTime.Now,
            //    Temperature = 69,
            //    AirHumidity =1337,
            //    AirPressure = 10
            //});
        }

        //public WeatherForecastController(ApplicationDbContext dbContext)
        //{
        //    _weatherData = WeatherData.GetInstance();
        //    _context = dbContext;
        //}

        /// <summary>
        /// Posts weatherforecast data
        /// </summary>
        /// <param name="weatherData"></param>
        /// <returns></returns>
        
        [HttpPost, Authorize]
        public async Task<ActionResult<WeatherForecast>> Post(WeatherForecast weatherData)
        {
            if (weatherData == null)
            {
                return BadRequest();
            }

            _context.weatherForecasts.Add(weatherData);
            await _context.SaveChangesAsync();

            await _chatHubContext.Clients.All.ReceiveNewWeatherData(weatherData);

            return Created("GetWeatherForecast",
                weatherData);
        }

        /// <summary>
        /// Get latest weatherforecast
        /// </summary>
        /// <returns>
        /// Latest weatherforecast
        /// </returns>
        [HttpGet("Latest")]
        public async Task<ActionResult<List<WeatherForecast>>> GetLatestAsync()
        {
            var latestForecast = await _context.weatherForecasts
                .Include(w => w.ObservedLocation)
                .OrderByDescending(w => w.WeatherForecastId)
                .Take(3)
                .ToListAsync();

            return latestForecast;
        }

        [HttpGet("{date}", Name = "AtDate")]
        public async Task<ActionResult<List<WeatherForecast>>> GetAtDateAsync(DateTime date)
        {

            var allWeatherAtDate = await _context.weatherForecasts
                .Where(w => w.Date == date)
                .Include(w => w.ObservedLocation)
                .ToListAsync();

            return allWeatherAtDate;
        }
        [HttpGet("{startDate}/{endDate}", Name = "AtDateRange")]
        public async Task<ActionResult<List<WeatherForecast>>> GetAtDateRangeAsync(DateTime startDate, DateTime endDate)
        {

            var allWeatherAtDate = await _context.weatherForecasts
                .Where(w => w.Date >= startDate && w.Date <= endDate)
                .Include(w => w.ObservedLocation)
                .ToListAsync();

            return allWeatherAtDate;
        }


    }
}
