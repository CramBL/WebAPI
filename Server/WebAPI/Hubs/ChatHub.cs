using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebAPI
{
    public interface IChat
    {
        Task ReceiveNewWeatherData(WeatherForecast newWeatherData);


    }
    public class ChatHub : Hub<IChat>
    {
        public async Task SendMessage(WeatherForecast newWeatherData)
        {
            await Clients.All.ReceiveNewWeatherData(newWeatherData);
        }
    }
}