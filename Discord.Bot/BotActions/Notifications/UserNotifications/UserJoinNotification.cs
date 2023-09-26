namespace Discord.Bot.BotActions.Notifications.UserNotifications;

using Discord.WebSocket;
using MediatR;

public class UserJoinNotification : INotification
{
    public SocketGuildUser User { get; }
    public DiscordSocketClient Client { get; }

    public UserJoinNotification(SocketGuildUser user, DiscordSocketClient client)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }
}