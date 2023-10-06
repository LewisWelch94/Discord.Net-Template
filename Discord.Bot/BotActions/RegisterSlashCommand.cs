namespace Discord.Bot.BotActions;

using Discord.Bot.BotActions.Models;
using Discord.Bot.SlashCommands;
using Discord.Bot.UserCommands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;

public static class RegisterSlashCommand
{
    public static async Task<BotListing> RegisterCommands(DiscordSocketClient client, SocketGuild? guild, BotListing listing)
    {
        try
        {
            var commands = GetCommands();
            BotListing cache;

            if(listing.BotInfos.Count != 0)
            {
                if (guild == null && listing.BotInfos[0].GuildId != null)
                {
                    var lastGuild = client.GetGuild((ulong)listing.BotInfos[0].GuildId!);
                    await lastGuild.BulkOverwriteApplicationCommandAsync(Array.Empty<ApplicationCommandProperties>());
                }
            }
            
            if (guild != null)
            {
                await guild.BulkOverwriteApplicationCommandAsync(Array.Empty<ApplicationCommandProperties>());
                await guild.BulkOverwriteApplicationCommandAsync(commands.ToArray());
                Console.WriteLine($"Slash commands has been registered to {guild.Name}");

                cache = HandleCache(guild, listing);
                return cache;
            }

            await client.BulkOverwriteGlobalApplicationCommandsAsync(Array.Empty<ApplicationCommandProperties>());
            await client.BulkOverwriteGlobalApplicationCommandsAsync(commands.ToArray());
            Console.WriteLine("Slash commands has been registered");

            cache = HandleCache(guild, listing);
            return cache;

        }
        catch (HttpException e)
        {
            var json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
            Console.WriteLine(json);
            throw;
        }
    }

    private static List<ApplicationCommandProperties> GetCommands()
    {
        return new List<ApplicationCommandProperties>()
        {
            // Slash Commands
            new TestSlashCommand().CreateSlashCommand().Build(),

            // User Commands
            new TestUserCommand().CreateUserCommand().Build(),
        };
    }

    private static BotListing HandleCache(SocketGuild? guild, BotListing listing)
    {
        if (guild != null)
        {
            listing.BotInfos.Clear();
            listing.BotInfos.Add(new BotListing.BotInfo
            {
                LastUsedInSoloGuildMode = true,
                GuildId = guild.Id,
            });

            return listing;
        }

        listing.BotInfos.Clear();
        listing.BotInfos.Add(new BotListing.BotInfo
        {
            LastUsedInSoloGuildMode = false,
            GuildId = null,
        });

        return listing;
    }
}