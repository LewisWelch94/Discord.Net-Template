namespace Discord.Bot;

using Discord.Bot.BotActions;
using Discord.Bot.BotActions.Configuration;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Text.Json;

public static class Bot
{
    public static async Task Main(string[] args)
    {
        await using var services = BotServices();
        var client = services.GetRequiredService<DiscordSocketClient>();
        client.Log += LogAsync;

        var listener = services.GetRequiredService<DiscordEventListener>();
        await listener.StartAsync();

        var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("appsettings.json"));
        await client.LoginAsync(TokenType.Bot, settings.Bot.BotToken);
        await client.StartAsync();

        await Task.Delay(-1);
    }

    private static ServiceProvider BotServices()
    {
        return new ServiceCollection()
        .AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(Bot)))
        .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
        {
            AlwaysDownloadUsers = true,
            MessageCacheSize = 100,
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent | GatewayIntents.GuildMembers | GatewayIntents.GuildBans,
            LogLevel = LogSeverity.Info
        }))
        .AddSingleton<DiscordEventListener>()
        .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
        .BuildServiceProvider();
    }

    private static Task LogAsync(LogMessage message)
    {
        var severity = message.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };

        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        Console.WriteLine(message);

        return Task.CompletedTask;
    }
}