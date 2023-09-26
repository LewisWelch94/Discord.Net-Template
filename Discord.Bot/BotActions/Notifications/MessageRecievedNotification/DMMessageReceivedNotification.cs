namespace Discord.Bot.BotActions.Notifications.MessageRecievedNotification;

using Discord.WebSocket;
using MediatR;

public class DMMessageReceivedNotification : INotification
{
    public SocketMessage Message { get; }

    public DMMessageReceivedNotification(SocketMessage message)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }
}