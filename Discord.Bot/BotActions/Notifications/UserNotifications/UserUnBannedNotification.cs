namespace Discord.Bot.BotActions.Notifications.UserNotifications;

using Discord.WebSocket;
using MediatR;

public class UserUnBannedNotification : INotification
{
    public SocketGuild Guild { get; }
    public SocketUser User { get; }

    public UserUnBannedNotification(SocketUser user, SocketGuild guild)
    {
        Guild = guild ?? throw new ArgumentNullException(nameof(guild));
        User = user ?? throw new ArgumentNullException(nameof(user));
    }
}