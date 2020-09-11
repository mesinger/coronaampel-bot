using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;

namespace Mesi.Covid.Austria.CoronaAmpel.Contract.Localization
{
    /// <summary>
    /// Transforms corona stoplight info to localized strings
    /// </summary>
    public interface ICoronaStopLightLocalizationService
    {
        /// <summary>
        /// Transforms corona stoplight levels to a localized string
        /// </summary>
        /// <param name="level"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        string GetForLevelAndLanguage(CoronaStopLightLevel level, string culture);
    }
}