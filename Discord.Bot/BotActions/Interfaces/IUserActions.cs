namespace Discord.Bot.BotActions.Interfaces;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using Discord.WebSocket;

public interface IUserActions
{
    Task UserJoined(UserJoinNotification notification);

    Task UserLeft(UserLeftNotification notification);

    Task UserBanned(UserBannedNotification notification);

    Task UserUnBanned(UserUnBannedNotification notification);
}