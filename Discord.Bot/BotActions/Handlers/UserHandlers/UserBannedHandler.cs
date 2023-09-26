namespace Discord.Bot.BotActions.Handlers.UserHandlers;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using Discord.Bot.UserActions;
using MediatR;

public class UserBannedHandler : INotificationHandler<UserBannedNotification>
{
    public async Task Handle(UserBannedNotification notification, CancellationToken cancellationToken)
        => await new UserActions().UserBanned(notification);
}