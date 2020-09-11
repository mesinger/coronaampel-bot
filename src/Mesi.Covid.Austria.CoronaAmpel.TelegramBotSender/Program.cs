using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

            var communeOptions = serviceProvider.GetRequiredService<IOptions<CommuneOptions>>().Value;
            var communeId = communeOptions.Id;

            if (string.IsNullOrWhiteSpace(communeId))
            {
                logger.LogWarning("Empty commune id specified");
                return;
            }

            logger.LogInformation("Requesting CoronaAmpel data for commune with id '{id}'", communeId);
            
            var getCoronaStopLightDataForCommune =
                serviceProvider.GetRequiredService<IGetCoronaStopLightDataForCommune>();

            var communeStatus = await getCoronaStopLightDataForCommune.GetCommuneStatus(communeId);

            if (communeStatus != null)
            {
                var messageService = serviceProvider.GetRequiredService<ICoronaStopLightMessageService>();
                await messageService.SendCommuneStatus(communeStatus);

                logger.LogInformation("Successfully sent message");
            }
            else
            {
                logger.LogInformation("Unable to get data for commune with id '{id}'", communeId);
            }
        }
    }
}