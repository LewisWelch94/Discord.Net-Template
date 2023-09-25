namespace Discord.Bot.BotActions.Interfaces;

using Discord.Bot.BotActions.Notifications;

public interface IButton
{
    ButtonBuilder Createbutton();

    Task Execute(ButtonNotification notification);
}