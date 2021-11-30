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

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                context.weatherForecasts.AddRange(
                    new WeatherForecast{
                        WeatherForecastId = 1,
                        AirHumidity = 1,
                        AirPressure = 1,
                        Date = new DateTime(),
                        ObservedLocation = new ObservedLocation(),
                        ObservedLocationId = 1,
                        Temperature = 1
                    },
                    new WeatherForecast{
                        WeatherForecastId = 2,
                        AirHumidity = 2,
                        AirPressure = 2,
                        Date = new DateTime(),
                        ObservedLocation = new ObservedLocation(),
                        ObservedLocationId = 2,
                        Temperature = 2
                    },
                    new WeatherForecast{
                        WeatherForecastId = 3,
                        AirHumidity = 3,
                        AirPressure = 3,
                        Date = new DateTime(),
                        ObservedLocation = new ObservedLocation(),
                        ObservedLocationId = 3,
                        Temperature = 3
                    }
                    );
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var WController = new WeatherForecastController(context);
                var latest = WController.GetLatestAsync();
                Assert.NotNull(latest);
                //Assert.Equal(
            }










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
