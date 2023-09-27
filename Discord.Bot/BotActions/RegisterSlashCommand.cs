namespace Discord.Bot.BotActions;

using Discord.Bot.SlashCommands;
using Discord.Bot.UserCommands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;

public static class RegisterSlashCommand
{
    public static async Task RegisterCommands(DiscordSocketClient client, SocketGuild? guild)
    {
        var slashCommands = GetSlashCommands();
        var userCommands = GetUserCommands();

        List<ApplicationCommandProperties> commands = new List<ApplicationCommandProperties>();
        await client.BulkOverwriteGlobalApplicationCommandsAsync(commands.ToArray());
        foreach (var command in slashCommands)
        {
            var newCommand = command.Build();
            try
            {
                
                
                if (guild != null)
                {
                    await guild.BulkOverwriteApplicationCommandAsync(commands.ToArray());
                    await guild.CreateApplicationCommandAsync(newCommand);
                    Console.WriteLine($"{newCommand.Name} Slash command has been registered to {guild.Name}");
                    continue;
                }

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
                if (guild != null)
                {
                    await guild.CreateApplicationCommandAsync(newCommand);
                    Console.WriteLine($"{newCommand.Name} User command has been registered to {guild.Name}");
                    continue;
                }

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