namespace Discord.Bot.BotActions.Handlers.MessageRecievedHandlers;

using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;
using Discord.Bot.Messages.DmMessageRecieved;
using MediatR;

public class DMMessageReceivedHandler : INotificationHandler<DMMessageReceivedNotification>
{
    public async Task Handle(DMMessageReceivedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Message.Content == "test") await new TestDMMessage().Execute(notification);
    }
}