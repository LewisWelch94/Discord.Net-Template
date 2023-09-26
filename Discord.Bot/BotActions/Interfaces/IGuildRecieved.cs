namespace Discord.Bot.BotActions.Interfaces;

using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;

public interface IGuildRecieved
{
    Task Execute(MessageReceivedNotification notification);
}