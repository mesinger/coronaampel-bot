using System.Threading.Tasks;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;

namespace Mesi.Covid.Austria.CoronaAmpel.Contract.Data
{
    /// <summary>
    /// Sends information about CoronaAmpel to a recipient
    /// </summary>
    public interface ICoronaStopLightMessageService
    {
        /// <summary>
        /// Sends covid data for a specific commune
        /// </summary>
        /// <param name="status">The data for the commune</param>
        Task SendCommuneStatus(CommuneCoronaStopLightStatus status);
    }
}