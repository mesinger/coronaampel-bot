using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;

namespace Mesi.Covid.Austria.CoronaAmpel.Services
{
    /// <summary>
    /// Retrieve CoronaAmpel data for a commune
    /// </summary>
    public interface IGetCoronaStopLightDataForCommune
    {
        /// <summary>
        /// Retrieve CoronaAmpel data for a commune
        /// </summary>
        /// <param name="communeId"></param>
        /// <returns></returns>
        Task<CommuneCoronaStopLightStatus> GetCommuneStatus(string communeId);
    }
}