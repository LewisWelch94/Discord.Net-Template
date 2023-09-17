namespace Discord.Bot.BotActions.Interfaces;

using Discord.Bot.BotActions.Notifications;

public interface IMessageRecieved
{
    Task Execute(MessageReceivedNotification notification);
}