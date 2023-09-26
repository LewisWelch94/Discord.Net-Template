namespace Discord.Bot.BotActions.Handlers.UserHandlers;

using Discord.Bot.BotActions.Notifications.UserNotifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class UserVoiceStateHandler : INotificationHandler<UserVoiceStateNotification>
{
    public async Task Handle(UserVoiceStateNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Old State: {notification.OldState.VoiceChannel}\n\nNew State: {notification.Newstate.VoiceChannel}\n\nUser: {notification.User.Username}");
    }
}