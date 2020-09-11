using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;

namespace Mesi.Covid.Austria.CoronaAmpel.Contract.Data
{
    /// <summary>
    /// Gets data for the austrian CoronaAmpel
    /// </summary>
    public interface ICoronaStopLightRepository
    {
        /// <summary>
        /// Retrieves the covid status for a given commune
        /// </summary>
        /// <param name="communeId">The identifier for a commune</param>
        /// <returns></returns>
        Task<CommuneCoronaStopLightStatus?> GetByCommuneId(string communeId);
    }
}