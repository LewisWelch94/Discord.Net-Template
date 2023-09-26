namespace Discord.Bot.Messages.DmMessageRecieved;

using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;

public class TestDMMessage : IDmRecieved
{
    public async Task Execute(DMMessageReceivedNotification notification)
    {
        var message = notification.Message as IUserMessage;
        await message.ReplyAsync("Hello from DM");
    }
}