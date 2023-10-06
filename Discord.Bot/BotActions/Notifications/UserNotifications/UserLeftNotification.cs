namespace Discord.Bot.BotActions.Notifications.UserNotifications;

using Discord.WebSocket;
using MediatR;

public class UserLeftNotification : INotification
{
    public SocketGuild Guild { get; }
    public SocketUser User { get; }

    public UserLeftNotification(SocketGuild guild, SocketUser user)
    {
        Guild = guild ?? throw new ArgumentNullException(nameof(guild));
        User = user ?? throw new ArgumentNullException(nameof(user));
    }
}