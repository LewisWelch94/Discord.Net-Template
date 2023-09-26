namespace Discord.Bot.BotActions;

using Discord.Bot.SlashCommands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;

public static class RegisterSlashCommand
{
    public static async Task RegisterCommands(DiscordSocketClient client)
    {
        var list = GetCommands();

        foreach (var command in list)
        {
            var newCommand = command.Build();
            try
            {
                await client.CreateGlobalApplicationCommandAsync(newCommand);
                Console.WriteLine($"{newCommand.Name} has been registered");
            }
            catch (HttpException e)
            {
                var json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
                Console.WriteLine(json);
                throw;
            }
        }
    }

    private static List<SlashCommandBuilder> GetCommands()
    {
        return new List<SlashCommandBuilder>()
        {
            new TestSlashCommand().CreateSlashCommand(),
        };
    }
}