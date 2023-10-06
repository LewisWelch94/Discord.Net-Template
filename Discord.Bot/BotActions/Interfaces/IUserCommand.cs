namespace Discord.Bot.BotActions.Interfaces;

using Discord.Bot.BotActions.Notifications.CommandNotifications;

public interface IUserCommand
{
    UserCommandBuilder CreateUserCommand();

    Task Execute(UserCommandNotification notification);
}