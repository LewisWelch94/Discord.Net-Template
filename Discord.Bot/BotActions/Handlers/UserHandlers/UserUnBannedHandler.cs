namespace Discord.Bot.BotActions.Handlers.UserHandlers;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using MediatR;

public class UserUnBannedHandler : INotificationHandler<UserUnBannedNotification>
{
    public async Task Handle(UserUnBannedNotification notification, CancellationToken cancellationToken)
        => await new UserActions().UserUnBanned(notification);
}