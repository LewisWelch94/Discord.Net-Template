namespace Discord.Bot.SlashCommands;

using Discord;
using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Notifications.CommandNotifications;

public class TestSlashCommand : ISlashCommand
{
    public SlashCommandBuilder CreateSlashCommand()
    {
        return new SlashCommandBuilder
        {
            Name = "test",
            Description = "this is my test"
        };
    }

    public async Task Execute(SlashCommandNotification notification)
    {
        await notification.Command.RespondAsync("test works");
    }
}