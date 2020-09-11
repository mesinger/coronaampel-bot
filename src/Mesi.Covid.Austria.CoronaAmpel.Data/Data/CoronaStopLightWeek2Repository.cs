using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;
using Mesi.Covid.Austria.CoronaAmpel.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Mesi.Covid.Austria.CoronaAmpel.Data.Data
{
    /// <summary>
    /// Implementation for the <see cref="ICoronaStopLightRepository"/> interface
    /// This implementation is called 'Week2' because the austrian government isn't consistent with their data,
    /// meaning the data format could change for future CoronaAmpel releases
    /// Current problems: Currently, there are not all communes in the dataset, means, a request for a given commune could find no data => defaults to warning level green
    /// General problems: Data schema could change from week to week
    /// </summary>
    public class CoronaStopLightWeek2Repository : ICoronaStopLightRepository
    {
        private readonly HttpClient _httpClient;
        private readonly CoronaAmpelOptions _coronaAmpelOptions;
        private readonly ILogger<CoronaStopLightWeek2Repository> _logger;

        public CoronaStopLightWeek2Repository(HttpClient httpClient, IOptions<CoronaAmpelOptions> coronaAmpelOptions, ILogger<CoronaStopLightWeek2Repository> logger)
        {
            _httpClient = httpClient;
            _coronaAmpelOptions = coronaAmpelOptions.Value;
            _logger = logger;
        }
        
        /// <inheritdoc />
        public async Task<CommuneCoronaStopLightStatus?> GetByCommuneId(string communeId)
        {
            var response = await _httpClient.GetStringAsync(_coronaAmpelOptions.DataEndpoint);
            var data = JsonConvert.DeserializeObject<IEnumerable<CoronaStopLightEntry>>(response)?.ToList();

            if (data == null || !data.Any())
            {
                _logger.LogWarning("Unable to get data for CoronaAmpel");
                return null;
            }

            var mostRecentEntry = data.OrderByDescending(entry => entry.Timestamp).FirstOrDefault();
            var communeEntry = mostRecentEntry.WarningLevels.FirstOrDefault(entry => entry.CommuneId == communeId);

            if (communeEntry == null)
            {
                _logger.LogWarning("Unable to find commune with id '{id}'.", communeId);
                return null;
            }

            var warningLevel = communeEntry.Level.AsCoronaStopLightLevel();

            if (warningLevel == null)
            {
                _logger.LogWarning("Unable to get valid CoronaAmpel value for integer '{level}'", communeEntry.Level);
                return null;
            }
            
            return new CommuneCoronaStopLightStatus(communeEntry.CommuneName, communeEntry.CommuneId, warningLevel.Value);
        }
    }
}