namespace Discord.Bot.BotActions.Handlers;

using Discord.Bot.BotActions.Notifications;
using Discord.Bot.Buttons;
using MediatR;

public class ButtonHandler : INotificationHandler<ButtonNotification>
{
    public async Task Handle(ButtonNotification notification, CancellationToken cancellationToken)
    {
        switch (notification.Button.Data.CustomId)
        {
            case "test":
                await new TestButton().Execute(notification);
                break;

            default:
                throw new ArgumentException($"{notification.Button.Data.CustomId} is not a button");
        }
    }
}