using Microsoft.Extensions.Hosting;

namespace MyBlazor.Client.Service
{
    public class CalculationService : BackgroundService
    {
        public static event Func<Task>? updateEvent;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine(".");
                await Task.Delay(100);
            }
            //throw new NotImplementedException();
        }
    }
}
