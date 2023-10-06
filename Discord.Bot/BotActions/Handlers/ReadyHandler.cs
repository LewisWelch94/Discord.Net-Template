namespace Discord.Bot.BotActions.Handlers;

using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Models;
using Discord.Bot.BotActions.Notifications;
using MediatR;

public class ReadyHandler : INotificationHandler<ReadyNotification>
{
    private readonly ICacheHelper CacheHelper;

    public ReadyHandler(ICacheHelper cacheHelper)
    {
        CacheHelper = cacheHelper;
    }

    public async Task Handle(ReadyNotification notification, CancellationToken cancellationToken)
    {
        var cache = CacheHelper.GetCache<BotListing>("bot", "botinfo.json");
        var newCache = await RegisterSlashCommand.RegisterCommands(notification.Client, notification.Guild, cache);
        await CacheHelper.SaveCache(newCache, "bot", "botinfo.json");
    }
}