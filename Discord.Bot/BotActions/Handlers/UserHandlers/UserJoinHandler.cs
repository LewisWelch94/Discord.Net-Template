namespace Discord.Bot.BotActions.Handlers.UserHandlers;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using Discord.Bot.UserActions;
using MediatR;

public class UserJoinHandler : INotificationHandler<UserJoinNotification>
{
    public async Task Handle(UserJoinNotification notification, CancellationToken cancellationToken)
        => await new UserActions().UserJoined(notification);
}