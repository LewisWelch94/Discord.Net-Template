namespace Discord.Bot.BotActions.Handlers;

using Discord.Bot.BotActions.Notifications;
using MediatR;

public class ReadyHandler : INotificationHandler<ReadyNotification>
{
    public async Task Handle(ReadyNotification notification, CancellationToken cancellationToken)
    {
        await RegisterSlashCommand.RegisterCommands(notification.Client, notification.Guild);
    }
}