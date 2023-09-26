namespace Discord.Bot.BotActions.Handlers.UserHandlers;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using MediatR;

public class UserLeftHandler : INotificationHandler<UserLeftNotification>
{
    public async Task Handle(UserLeftNotification notification, CancellationToken cancellationToken)
        => await new UserActions().UserLeft(notification);
}