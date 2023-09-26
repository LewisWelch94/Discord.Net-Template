namespace Discord.Bot.BotActions;

using Discord.Bot.SlashCommands;
using Discord.Bot.UserCommands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;

public static class RegisterSlashCommand
{
    public static async Task RegisterCommands(DiscordSocketClient client)
    {
        var slashCommands = GetSlashCommands();
        var userCommands = GetUserCommands();

        foreach (var command in slashCommands)
        {
            var newCommand = command.Build();
            try
            {
                await client.CreateGlobalApplicationCommandAsync(newCommand);
                Console.WriteLine($"{newCommand.Name} Slash command has been registered");
            }
            catch (HttpException e)
            {
                var json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
                Console.WriteLine(json);
                throw;
            }
        }

        foreach (var command in userCommands)
        {
            var newCommand = command.Build();
            try
            {
                await client.CreateGlobalApplicationCommandAsync(newCommand);
                Console.WriteLine($"{newCommand.Name} User command has been registered");
            }
            catch (HttpException e)
            {
                var json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
                Console.WriteLine(json);
                throw;
            }
        }
    }

    private static List<SlashCommandBuilder> GetSlashCommands()
    {
        return new List<SlashCommandBuilder>()
        {
            new TestSlashCommand().CreateSlashCommand(),
        };
    }

    private static List<UserCommandBuilder> GetUserCommands()
    {
        return new List<UserCommandBuilder>
        {
            new TestUserCommand().CreateUserCommand(),
        };
    }
}