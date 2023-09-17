namespace Discord.Bot.BotActions.Notifications;

using Discord.WebSocket;
using MediatR;

public class ButtonNotification : INotification
{
    public SocketMessageComponent Button { get; }

    public ButtonNotification(SocketMessageComponent button)
    {
        Button = button ?? throw new ArgumentNullException(nameof(button));
    }
}