namespace Mesi.Covid.Austria.CoronaAmpel.Contract.Models
{
    /// <summary>
    /// Austrian warning levels for the CoronaAmpel
    /// </summary>
    public enum CoronaStopLightLevel
    {
        Green = 1,
        Yellow,
        Orange,
        Red
    }
    
    public static partial class DataConversionExtensions
    {
        public static CoronaStopLightLevel? AsCoronaStopLightLevel(this int number)
        {
            return number switch
            {
                1 => CoronaStopLightLevel.Green,
                2 => CoronaStopLightLevel.Yellow,
                3 => CoronaStopLightLevel.Orange,
                4 => CoronaStopLightLevel.Red,
                _ => null
            };
        }
    }
}