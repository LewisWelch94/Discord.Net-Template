namespace Discord.Bot.BotActions.Handlers.UserHandlers;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using MediatR;

internal class UserUpdatedHandler : INotificationHandler<UserUpdatedNotification>
{
    public async Task Handle(UserUpdatedNotification notification, CancellationToken cancellationToken)
    {
        var channel = await notification.Client.GetChannelAsync(1111219523056574524) as ITextChannel;
        await channel.SendMessageAsync($"User has been updated\n\n Old: ${notification.PreviousUser}\n\nNew: ${notification.CurrentUser}");
    }
}