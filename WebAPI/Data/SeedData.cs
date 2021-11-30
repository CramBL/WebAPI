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
                    Date = DateTime.Today,

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
                    Date = DateTime.Today,

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
                    Date = DateTime.Today,

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
                new WeatherForecast()
                {
                    Date = DateTime.Today.AddDays(-2),

                    Temperature = -3,
                    AirHumidity = 10,
                    AirPressure = 10,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 11,
                        Longitude = 11,
                    }


                },
                new WeatherForecast()
                {
                    Date = DateTime.Today.AddDays(-3),

                    Temperature = 100,
                    AirHumidity = 11,
                    AirPressure = 11,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 11,
                        Longitude = 11,
                    }


                },
                new WeatherForecast()
                {
                    Date = DateTime.Today.AddDays(-5),

                    Temperature = -4,
                    AirHumidity = 11,
                    AirPressure = 4,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 11,
                        Longitude = 11,
                    }


                },
                new WeatherForecast()
                {
                    Date = DateTime.Today.AddDays(-10),

                    Temperature = 32,
                    AirHumidity = 11,
                    AirPressure = 11,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 11,
                        Longitude = 11,
                    }


                },

                new WeatherForecast()
                {
                    Date = DateTime.Today.AddDays(-15),

                    Temperature = 49,
                    AirHumidity = 90,
                    AirPressure = 700,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 11,
                        Longitude = 11,
                    }


                },
                new WeatherForecast()
                {
                    Date = new DateTime( 2021, 10, 10, 0, 0, 0 ),

                    Temperature = 49,
                    AirHumidity = 90,
                    AirPressure = 700,

                    ObservedLocation = new Models.ObservedLocation()
                    {
                        LocationName = "Aarhus",
                        Latitude = 11,
                        Longitude = 11,
                    }
                },
                new WeatherForecast()
                {
                    Date = new DateTime( 2021, 10, 8, 0, 0, 0 ),

                    Temperature = 11,
                    AirHumidity = 95,
                    AirPressure = 0,

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
