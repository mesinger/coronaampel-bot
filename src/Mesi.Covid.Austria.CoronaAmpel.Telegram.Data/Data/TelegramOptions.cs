namespace Mesi.Covid.Austria.CoronaAmpel.Telegram.Data.Data
{
    public class TelegramOptions
    {
        public string ApiBaseUrl { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string ChatId { get; set; } = null!;
        public string ParseMode { get; set; } = null!;
        public string Format { get; set; } = null!;
    }
}