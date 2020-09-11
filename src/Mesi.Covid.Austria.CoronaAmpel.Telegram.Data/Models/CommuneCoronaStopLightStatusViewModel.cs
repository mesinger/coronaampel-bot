using Newtonsoft.Json;

namespace Mesi.Covid.Austria.CoronaAmpel.Telegram.Data.Models
{
    internal class CommuneCoronaStopLightStatusViewModel
    {
        public CommuneCoronaStopLightStatusViewModel(string chatId, string messageFormat, string warningLevelAsString, string parseMode)
        {
            ChatId = chatId;
            MessageFormat = messageFormat;
            WarningLevelAsString = warningLevelAsString;
            ParseMode = parseMode;
            
            Text = string.Format(MessageFormat, WarningLevelAsString);
            _serialized = JsonConvert.SerializeObject(this);
        }

        [JsonProperty("chat_id")]
        public string ChatId { get; }

        [JsonProperty("parse_mode")]
        public string ParseMode { get; set; }
        
        [JsonIgnore]
        public string MessageFormat { get; }
        
        [JsonIgnore]
        public string WarningLevelAsString { get; }

        [JsonProperty("text")]
        public string Text { get; }

        [JsonIgnore] 
        private readonly string _serialized;

        public string AsJson() => _serialized;
    }
}