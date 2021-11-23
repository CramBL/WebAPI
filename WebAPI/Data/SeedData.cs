using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data
{
    public class SeedData
    {

        public SeedData(ApplicationDbContext db)
        {
            SeedWeather(db);
        }

        private static void SeedWeather(ApplicationDbContext db)
        {
            if (db.weatherForecasts.Any())
                return;

            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>()

            {
                new WeatherForecast()
                {
                    Date = DateTime.Now,

                    Temperature = 20,
                    AirHumidity = 10,
                    AirPressure = 11,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 10,
                        Longitude = 11,
                    }


                },
                new WeatherForecast()
                {
                    Date = DateTime.Now,

                    Temperature = 99,
                    AirHumidity = 100,
                    AirPressure = 19,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Skagen",
                        Latitude = 12,
                        Longitude = 11,
                    }


                },
                new WeatherForecast()
                {
                    Date = DateTime.Now,

                    Temperature = 1,
                    AirHumidity = 11,
                    AirPressure = 11,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 11,
                        Longitude = 11,
                    }


                },

            };

            foreach (var weatherforecast in weatherForecasts)
            {
                db.weatherForecasts.Add(weatherforecast);
            }
            db.SaveChanges();

        }

    }
}
