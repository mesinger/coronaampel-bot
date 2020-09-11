using Newtonsoft.Json;

namespace Mesi.Covid.Austria.CoronaAmpel.Data.Models
{
    public class CoronaStopLightWarningLevel
    {
        [JsonProperty("Region")]
        public string Region { get; set; } = null!;
        
        [JsonProperty("GKZ")]
        public string CommuneId { get; set; } = null!;
        
        [JsonProperty("Name")]
        public string CommuneName { get; set; } = null!;
        
        [JsonProperty("Warnstufe")]
        public int Level { get; set; }
    }
}