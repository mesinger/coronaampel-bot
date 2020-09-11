using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mesi.Covid.Austria.CoronaAmpel.Data.Models
{
    public class CoronaStopLightEntry
    {
        [JsonProperty("Stand")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("Warnstufen")]
        public IEnumerable<CoronaStopLightWarningLevel> WarningLevels { get; set; }
    }
}