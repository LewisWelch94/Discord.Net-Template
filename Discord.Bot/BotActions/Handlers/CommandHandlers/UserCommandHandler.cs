namespace Discord.Bot.BotActions.Handlers.CommandHandlers;

using Discord.Bot.BotActions.Notifications.CommandNotifications;
using Discord.Bot.UserCommands;
using MediatR;

public class UserCommandHandler : INotificationHandler<UserCommandNotification>
{
    public async Task Handle(UserCommandNotification notification, CancellationToken cancellationToken)
    {
        switch (notification.Command.CommandName)
        {
            case "test":
                await new TestUserCommand().Execute(notification);
                break;

            default:
                throw new ArgumentNullException($"{notification.Command.CommandName} is not a command");
        }
    }
}