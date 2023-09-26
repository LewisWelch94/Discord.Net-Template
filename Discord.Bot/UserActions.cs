namespace Discord.Bot;

using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Notifications.UserNotifications;

public class UserActions : IUserActions
{
    public async Task UserBanned(UserBannedNotification notification)
    {
        var channel = notification.Guild.GetTextChannel(1111219523056574524) as ITextChannel;
        await channel.SendMessageAsync($"{notification.User.Username} has been banned!");
    }

    public async Task UserJoined(UserJoinNotification notification)
    {
        var channel = notification.Client.GetChannel(1111219523056574524) as ITextChannel;
        await channel.SendMessageAsync($"{notification.User.Username} has joined!");
    }

    public async Task UserLeft(UserLeftNotification notification)
    {
        var channel = notification.Guild.GetTextChannel(1111219523056574524) as ITextChannel;
        await channel.SendMessageAsync($"{notification.User.Username} has left!");
    }

    public async Task UserUnBanned(UserUnBannedNotification notification)
    {
        var channel = notification.Guild.GetTextChannel(1111219523056574524) as ITextChannel;
        await channel.SendMessageAsync($"{notification.User.Username} has been unbanned!");
    }
}