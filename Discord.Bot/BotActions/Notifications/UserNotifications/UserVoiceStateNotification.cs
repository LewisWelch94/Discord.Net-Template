namespace Discord.Bot.BotActions.Notifications.UserNotifications;

using Discord.WebSocket;
using MediatR;

public class UserVoiceStateNotification : INotification
{
    public SocketUser User { get; }
    public SocketVoiceState OldState { get; }
    public SocketVoiceState Newstate { get; }

    public UserVoiceStateNotification(SocketUser user, SocketVoiceState oldState, SocketVoiceState newstate)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        OldState = oldState;
        Newstate = newstate;
    }
}