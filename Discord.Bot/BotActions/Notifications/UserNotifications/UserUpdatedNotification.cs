namespace Discord.Bot.BotActions.Notifications.UserNotifications;

using Discord.WebSocket;
using MediatR;

public class UserUpdatedNotification : INotification
{
    public SocketUser PreviousUser { get; }
    public SocketUser CurrentUser { get; }
    public DiscordSocketClient Client { get; }

    public UserUpdatedNotification(SocketUser previousUser, SocketUser currentUser, DiscordSocketClient client)
    {
        PreviousUser = previousUser ?? throw new ArgumentNullException(nameof(previousUser));
        CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }
}