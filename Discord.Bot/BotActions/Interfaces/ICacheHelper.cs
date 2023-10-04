namespace Discord.Bot.BotActions.Interfaces;

public interface ICacheHelper
{
    T GetCache<T>(string key, string file) where T : new();

    Task SaveCache<T>(T cache, string key, string file) where T : new();
}