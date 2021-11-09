using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data
{
    public class WeatherData
    {
        private static WeatherData instance = null;
        private List<WeatherForecast> _weatherForecastList = new();
        
        private WeatherData()
        {
           
            
        }

        // Implement singleton design pattern
        static public WeatherData GetInstance()
        {
            if (instance == null)
                instance = new WeatherData();
            return instance;
        }

        public int AddWeatherForecast(WeatherForecast weatherforecast)
        {
            _weatherForecastList.Add(weatherforecast);

            return _weatherForecastList.Count() - 1;
        }

        public WeatherForecast ReturnLatestWeatherForecast()
        {
            if (_weatherForecastList.Count == 0)
                return null;

            return  _weatherForecastList[^1];
        }

        public WeatherForecast ReturnAtIndex(int index)
        {
            if (index < _weatherForecastList.Count())
                return _weatherForecastList[index];
            else
                return null;
        }

        public List<WeatherForecast> getAllWeatherForecasts()
        {
            if (_weatherForecastList.Count == 0)
                return null;
            return _weatherForecastList;
        }

        public List<WeatherForecast> GetWeatherForecastAtDate(DateTime date)
        {
            if (date == null)
                return null;

            List<WeatherForecast> weatherForecastsAtDate = new();

            var shortDate = date.ToShortDateString();

            foreach (var WeatherForecast in _weatherForecastList)
            {
                var tempDate = WeatherForecast.Date.ToShortDateString();
                if (shortDate == tempDate)
                    weatherForecastsAtDate.Add(WeatherForecast);
            }

            return weatherForecastsAtDate;
        }
 

    }
}
