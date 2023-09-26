namespace Discord.Bot.BotActions.Handlers.UserHandlers;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using MediatR;

public class UserJoinHandler : INotificationHandler<UserJoinNotification>
{
    public async Task Handle(UserJoinNotification notification, CancellationToken cancellationToken)
        => await new UserActions().UserJoined(notification);
}