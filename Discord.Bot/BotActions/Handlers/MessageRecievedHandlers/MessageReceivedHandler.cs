namespace Discord.Bot.BotActions.Handlers.MessageRecievedHandlers;

using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;
using Discord.Bot.Messages.GuildMessageRecieved;
using MediatR;

public class MessageReceivedHandler : INotificationHandler<MessageReceivedNotification>
{
    public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Message.Content == "test") await new TestGuildMessage().Execute(notification);
    }
}