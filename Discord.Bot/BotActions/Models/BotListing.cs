namespace Discord.Bot.BotActions.Models;

public class BotListing
{
    public List<BotInfo> BotInfos { get; set; } = new List<BotInfo>();

    public class BotInfo
    {
        public bool LastUsedInSoloGuildMode { get; set; }
        public ulong? GuildId { get; set; }
    }
}