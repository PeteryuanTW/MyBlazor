using Microsoft.AspNetCore.Mvc;
using MyBlazor.Shared.DataClass;
using MyBlazor.Shared;

namespace MyBlazor.Server.Controllers
{
    public class GAController : ControllerBase
    {
        public IEnumerable<WeatherForecast> ServideTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine(".");
            }
            return Enumerable.Empty<WeatherForecast>();
        }
    }
}
