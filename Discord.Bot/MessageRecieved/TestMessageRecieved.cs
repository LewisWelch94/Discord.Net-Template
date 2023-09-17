namespace Discord.Bot.MessageRecieved;

using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Notifications;

public class TestMessageRecieved : IMessageRecieved
{
    public async Task Execute(MessageReceivedNotification notification)
    {
        var message = notification.Message as IUserMessage;
        await message.ReplyAsync("hello");
    }
}