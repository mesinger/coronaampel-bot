using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mesi.Covid.Austria.CoronaAmpel.TelegramBotSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var startup = new Startup(args);
            
            startup.ConfigureLogging();

            await using var serviceProvider = startup.ConfigureServices();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            
            logger.LogInformation("Starting CoronaAmpel telegram bot sender");
            
            var getCoronaStopLightDataForCommune =
                serviceProvider.GetRequiredService<IGetCoronaStopLightDataForCommune>();

            var communeStatus = await getCoronaStopLightDataForCommune.GetCommuneStatus("701");

            var messageService = serviceProvider.GetRequiredService<ICoronaStopLightMessageService>();
            await messageService.SendCommuneStatus(communeStatus);

            logger.LogInformation("Finished");
        }
    }
}