namespace Discord.Bot.BotActions;

using Discord.Bot.BotActions.Configuration;
using Discord.Bot.BotActions.Notifications;
using Discord.Bot.BotActions.Notifications.CommandNotifications;
using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;
using Discord.Bot.BotActions.Notifications.UserNotifications;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

public class DiscordEventListener
{
    private readonly CancellationToken CancellationToken;
    private readonly DiscordSocketClient Client;
    private readonly IServiceScopeFactory ServiceScope;

    public SocketGuild? Guild { get; private set; } = null!;
    public Settings Settings { get; set; } = JsonSerializer.Deserialize<Settings>(File.ReadAllText("appsettings.json"))!;

    public DiscordEventListener(DiscordSocketClient client, IServiceScopeFactory serviceScope)
    {
        Client = client;
        ServiceScope = serviceScope;
        CancellationToken = new CancellationTokenSource().Token;
    }

    private IMediator Mediator
    {
        get
        {
            var scope = ServiceScope.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IMediator>();
        }
    }

    public Task StartAsync()
    {
        Client.Ready += OnReadyAsync;
        Client.MessageReceived += OnMessageReceivedAsync;
        Client.SlashCommandExecuted += OnSlashCommandExecutedAsync;
        Client.UserVoiceStateUpdated += OnUserVoiceStateupdate;
        Client.ButtonExecuted += OnButtonExecutedAsync;
        Client.UserJoined += OnUserJoinAsync;
        Client.UserLeft += OnUserLeftAsync;
        Client.UserBanned += OnUserBannedAsync;
        Client.UserUnbanned += OnUserUnBannedAsync;
        Client.UserUpdated += OnUserUpdatedAsync;
        Client.UserCommandExecuted += OnUserCommandAsync;

        return Task.CompletedTask;
    }

    private static SocketGuild? GetGuild(DiscordSocketClient client, Settings settings)
    {
        if (!settings.Bot.SingleGuildMode) return null;
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentException.ThrowIfNullOrEmpty(settings.Bot.GuildId);

        var parseGuildId = UInt64.TryParse($"{settings.Bot.GuildId}", out ulong guildId);
        if (!parseGuildId) throw new Exception($"{settings.Bot.GuildId} is not a valid ulong");

        try
        {
            var guild = client.GetGuild(guildId);
            return guild;
        }
        catch (Exception e)
        {
            e.Data.Add("GetGuild", "Guild doesnt not exist or GuildId is wrong");
            throw;
        }
    }

    private Task OnMessageReceivedAsync(SocketMessage arg)
    {
        if (arg.Author.IsBot || arg.Author.IsWebhook) return Task.Run(() => Console.WriteLine(""));
        var channel = arg.Channel.GetType();
        var message = arg as SocketUserMessage;
        if (channel == typeof(SocketDMChannel)) return Mediator.Publish(new DMMessageReceivedNotification(message!), CancellationToken);
        if (channel == typeof(SocketTextChannel)) return Mediator.Publish(new MessageReceivedNotification(message!), CancellationToken);
        return Task.Run(() => Console.WriteLine(""));
    }

    private Task OnReadyAsync()
    {
        var getGuild = GetGuild(Client, Settings);
        Guild = getGuild;
        var client = Client;
        return Mediator.Publish(new ReadyNotification(client, Guild), CancellationToken);
    }

    private Task OnSlashCommandExecutedAsync(SocketSlashCommand arg)
        => Mediator.Publish(new SlashCommandNotification(arg), CancellationToken);

    private Task OnUserVoiceStateupdate(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
        => Mediator.Publish(new UserVoiceStateNotification(user, oldState, newState), CancellationToken);

    private Task OnButtonExecutedAsync(SocketMessageComponent arg)
        => Mediator.Publish(new ButtonNotification(arg), CancellationToken);

    private Task OnUserJoinAsync(SocketGuildUser arg)
    {
        var client = Client;
        if (Settings.Bot.SingleGuildMode)
        {
            if (arg.Guild != Guild) return null!;
            return Mediator.Publish(new UserJoinNotification(arg, client), CancellationToken);
        }

        return Mediator.Publish(new UserJoinNotification(arg, client), CancellationToken);
    }

    private Task OnUserLeftAsync(SocketGuild arg1, SocketUser arg2)
        => Mediator.Publish(new UserLeftNotification(arg1, arg2), CancellationToken);

    private Task OnUserBannedAsync(SocketUser arg1, SocketGuild arg2)
        => Mediator.Publish(new UserBannedNotification(arg1, arg2), CancellationToken);

    private Task OnUserUnBannedAsync(SocketUser arg1, SocketGuild arg2)
        => Mediator.Publish(new UserUnBannedNotification(arg1, arg2), CancellationToken);

    private Task OnUserCommandAsync(SocketUserCommand arg)
        => Mediator.Publish(new UserCommandNotification(arg), CancellationToken);

    private Task OnUserUpdatedAsync(SocketUser arg1, SocketUser arg2)
    {
        var client = Client;
        return Mediator.Publish(new UserUpdatedNotification(arg1, arg2, client), CancellationToken);
    }
}