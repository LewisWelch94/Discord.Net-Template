namespace Discord.Bot.BotActions.Notifications.CommandNotifications;

using Discord.WebSocket;
using MediatR;

public class UserCommandNotification : INotification
{
    public SocketUserCommand Command { get; }

    public UserCommandNotification(SocketUserCommand command)
    {
        Command = command ?? throw new ArgumentNullException(nameof(command));
    }
}