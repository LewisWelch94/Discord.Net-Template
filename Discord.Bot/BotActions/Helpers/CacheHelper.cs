namespace Discord.Bot.BotActions.Helpers;

using Discord.Bot.BotActions.Interfaces;
using JsonFlatFileDataStore;

public class CacheHelper : ICacheHelper
{
    public T GetCache<T>(string key, string file) where T : new()
    {
        using (var store = new DataStore(file))
        {
            try
            {
                return store.GetItem<T>(key);
            }
            catch
            {
                return new T();
            }
        }
    }

    public async Task SaveCache<T>(T cache, string key, string file) where T : new()
    {
        using (var store = new DataStore(file))
        {
            await store.ReplaceItemAsync(key, cache, true);
        }
    }
}