using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Models;
using Xunit;
using SQLitePCL;

namespace WebAPI.Tests
{
    public class WeatherForecastControllerTests
    {
        
        [Fact]
        public void canReturnLatestWeatherForecast()
        {
            var connection = new SqliteConnection("Data source = :memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;












            //var wfC = new WeatherForecastController();
            //var w = new WeatherForecast {
            //    WeatherForecastId = 12,
            //    AirHumidity = 32,
            //    AirPressure = 4,
            //    Date = new DateTime(),
            //    ObservedLocation = new ObservedLocation(),
            //    ObservedLocationId = 354363,
            //    Temperature = 666};


            //Assert.Equal(w, w);
        }
    }
}
