namespace Discord.Bot.BotActions.Handlers;

using Discord.Bot.BotActions.Notifications;
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