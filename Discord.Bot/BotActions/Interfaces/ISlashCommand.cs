namespace Discord.Bot.BotActions.Interfaces;

using Discord.Bot.BotActions.Notifications;

public interface ISlashCommand
{
    SlashCommandBuilder CreateSlashCommand();

    Task Execute(SlashCommandNotification notification);
}