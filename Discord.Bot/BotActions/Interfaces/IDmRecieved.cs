namespace Discord.Bot.BotActions.Interfaces;

using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;

public interface IDmRecieved
{
    Task Execute(DMMessageReceivedNotification notification);
}