using MyBlazor.Shared.DataClass;

namespace MyBlazor.Server.Controllers
{
    public class GAController: IHostedService
    {
        private readonly HttpClient httpClient;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("start");
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("end");
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
        public GAController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<WoInfo> ServideTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine(".");
            }
            await Task.Delay(1000);
            return new WoInfo("","",1,DateTime.Now, DateTime.Now);
        }


    }
}
