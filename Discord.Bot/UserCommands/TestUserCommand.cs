namespace Discord.Bot.UserCommands;

using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Notifications.CommandNotifications;

public class TestUserCommand : IUserCommand
{
    public UserCommandBuilder CreateUserCommand()
    {
        return new UserCommandBuilder()
        {
            Name = "test",
        };
    }

    public async Task Execute(UserCommandNotification notification)
    {
        await notification.Command.RespondAsync("User command works");
    }
}