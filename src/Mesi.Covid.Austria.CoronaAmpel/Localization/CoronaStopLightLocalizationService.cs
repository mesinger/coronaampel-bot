using System;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Localization;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Models;

namespace Mesi.Covid.Austria.CoronaAmpel.Localization
{
    /// <inheritdoc />
    public class CoronaStopLightLocalizationService : ICoronaStopLightLocalizationService
    {
        /// <inheritdoc />
        public string GetForLevelAndLanguage(CoronaStopLightLevel status, string culture)
        {
            return culture switch
            {
                "de-AT" => German(status),
                _ => English(status)
            };
        }

        private static string German(CoronaStopLightLevel status)
        {
            return status switch
            {
                CoronaStopLightLevel.Green => "Grün",
                CoronaStopLightLevel.Yellow => "Gelb",
                CoronaStopLightLevel.Orange => "Orange",
                CoronaStopLightLevel.Red => "Rot",
                _ => string.Empty
            };
        }
        
        private static string English(CoronaStopLightLevel status)
        {
            return status switch
            {
                CoronaStopLightLevel.Green => "Green",
                CoronaStopLightLevel.Yellow => "Yellow",
                CoronaStopLightLevel.Orange => "Orange",
                CoronaStopLightLevel.Red => "Red",
                _ => string.Empty
            };
        }
    }
}