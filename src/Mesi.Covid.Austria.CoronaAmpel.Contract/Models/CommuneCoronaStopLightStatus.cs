namespace Mesi.Covid.Austria.CoronaAmpel.Contract.Models
{
    /// <summary>
    /// Represents the current corona situation (by the CoronaAmpel) in a commune
    /// </summary>
    public class CommuneCoronaStopLightStatus
    {
        public CommuneCoronaStopLightStatus(string name, string communeId, CoronaStopLightLevel warningLevel)
        {
            Name = name;
            CommuneId = communeId;
            WarningLevel = warningLevel;
        }

        public string Name { get; }
        public string CommuneId { get; }
        public CoronaStopLightLevel WarningLevel { get; }
    }
}