namespace Discord.Bot.BotActions.Notifications;

using Discord.WebSocket;
using MediatR;

public class ReadyNotification : INotification
{
    public DiscordSocketClient Client { get; }
    public SocketGuild? Guild { get; }

    public ReadyNotification(DiscordSocketClient client, SocketGuild? guild)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        Guild = guild;
    }
}