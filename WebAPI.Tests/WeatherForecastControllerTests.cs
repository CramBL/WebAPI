using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Models;
using Xunit;
using SQLitePCL;
using System.Collections.Generic;

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
                var weatherForecastList = latest.Result.Value;
                
                //Assert.NotNull(latest);
                Assert.Equal(3, weatherForecastList.Count);
            }
        }

        [Fact]
        public void canReturnWeatherForecastAtDate()
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
                        Date = new DateTime(2001,01,01),
                        ObservedLocation = new ObservedLocation(),
                        ObservedLocationId = 1,
                        Temperature = 1
                    },
                    new WeatherForecast{
                        WeatherForecastId = 2,
                        AirHumidity = 2,
                        AirPressure = 2,
                        Date = new DateTime(2002,02,02),
                        ObservedLocation = new ObservedLocation(),
                        ObservedLocationId = 2,
                        Temperature = 2
                    },
                    new WeatherForecast{
                        WeatherForecastId = 3,
                        AirHumidity = 3,
                        AirPressure = 3,
                        Date = new DateTime(2003, 03, 03),
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
                DateTime testDate = new DateTime(2003,03,03);
                var WeatherForecastsAtDate = WController.GetAtDateAsync(testDate);
                var weatherForecastListAtDate = WeatherForecastsAtDate.Result.Value;
                
                Assert.Collection(weatherForecastListAtDate,
                    forecast => Assert.Equal(testDate, forecast.Date));
            }
        }
    }
}
