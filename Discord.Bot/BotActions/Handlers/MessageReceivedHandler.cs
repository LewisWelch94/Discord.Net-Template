namespace Discord.Bot.BotActions.Handlers;

using Discord.Bot.BotActions.Notifications;
using Discord.Bot.MessageRecieved;
using MediatR;

public class MessageReceivedHandler : INotificationHandler<MessageReceivedNotification>
{
    public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Message.Content == "test") await new TestMessageRecieved().Execute(notification);
    }
}