namespace Discord.Bot.Messages.GuildMessageRecieved;

using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;

public class TestGuildMessage : IGuildRecieved
{
    public async Task Execute(MessageReceivedNotification notification)
    {
        var message = notification.Message as IUserMessage;
        await message.ReplyAsync("Hello From guild channel");
    }
}