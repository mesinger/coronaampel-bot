using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Localization;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;
using Mesi.Covid.Austria.CoronaAmpel.Telegram.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mesi.Covid.Austria.CoronaAmpel.Telegram.Data.Data
{
    /// <summary>
    /// Sends covid information to telegram
    /// </summary>
    public class CoronaStopLightTelegramMessageService : ICoronaStopLightMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly ICoronaStopLightLocalizationService _localizationService;
        private readonly ILogger<CoronaStopLightTelegramMessageService> _logger;
        private readonly TelegramOptions _telegramOptions;
        private readonly LocalizationOptions _localizationOptions;

        public CoronaStopLightTelegramMessageService(HttpClient httpClient, ICoronaStopLightLocalizationService localizationService, IOptions<TelegramOptions> telegramOptions, IOptions<LocalizationOptions> localizationOptions,
            ILogger<CoronaStopLightTelegramMessageService> logger)
        {
            _httpClient = httpClient;
            _localizationService = localizationService;
            _logger = logger;
            _telegramOptions = telegramOptions.Value;
            _localizationOptions = localizationOptions.Value;
        }

        /// <inheritdoc />
        public async Task SendCommuneStatus(CommuneCoronaStopLightStatus status)
        {
            var vm = new CommuneCoronaStopLightStatusViewModel(_telegramOptions.ChatId, _telegramOptions.Format, _localizationService.GetForLevelAndLanguage(status.WarningLevel, _localizationOptions.Culture), _telegramOptions.ParseMode);

            var response = await _httpClient.PostAsync(
                $"{_telegramOptions.ApiBaseUrl}bot{_telegramOptions.AccessToken}/sendMessage",
                new StringContent(vm.AsJson(), Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("Successfully sent message via telegram");
            }
            else
            {
                _logger.LogInformation("Sending of message via telegram failed with code '{code}'",
                    response.StatusCode);
            }
        }
    }
}