namespace Discord.Bot.BotActions.Configuration;

using System.Text.Json.Serialization;

public class Settings
{
    [JsonPropertyName("Bot")]
    public BotSettings Bot { get; set; } = null!;

    public class BotSettings
    {
        [JsonPropertyName("Token")]
        public string BotToken { get; set; } = string.Empty;
    }
}