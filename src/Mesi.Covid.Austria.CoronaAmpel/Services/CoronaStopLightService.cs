using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;
using Microsoft.Extensions.Logging;

namespace Mesi.Covid.Austria.CoronaAmpel.Services
{
    /// <summary>
    /// Implementation for corona stoplight related use cases
    /// </summary>
    public class CoronaStopLightService : IGetCoronaStopLightDataForCommune
    {
        private readonly ICoronaStopLightRepository _coronaStopLightRepository;
        private readonly ILogger<CoronaStopLightService> _logger;

        public CoronaStopLightService(ICoronaStopLightRepository coronaStopLightRepository, ILogger<CoronaStopLightService> logger)
        {
            _coronaStopLightRepository = coronaStopLightRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<CommuneCoronaStopLightStatus> GetCommuneStatus(string communeId)
        {
            var status = await _coronaStopLightRepository.GetByCommuneId(communeId);

            if (status == null)
            {
                _logger.LogInformation("CoronaAmpel did not respond any information about commune with id '{id}'. Default warning level to green.");
                return new CommuneCoronaStopLightStatus(string.Empty, communeId, CoronaStopLightLevel.Green);
            }

            _logger.LogInformation("Retrieved CoronaAmpel level '{level}' for commune '{name}'.", status.WarningLevel, status.Name);
            return status;
        }
    }
}