namespace Discord.Bot.BotActions;

using Discord.Bot.BotActions.Notifications;
using Discord.Bot.BotActions.Notifications.MessageRecievedNotification;
using Discord.Bot.BotActions.Notifications.UserNotifications;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class DiscordEventListener
{
    private readonly CancellationToken CancellationToken;
    private readonly DiscordSocketClient Client;
    private readonly IServiceScopeFactory ServiceScope;

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

        return Task.CompletedTask;
    }

    private Task OnMessageReceivedAsync(SocketMessage arg)
    {
        var channel = arg.Channel.GetType();
        var message = arg as SocketUserMessage;
        if (channel == typeof(SocketDMChannel)) return Mediator.Publish(new DMMessageReceivedNotification(message!), CancellationToken);
        if (channel == typeof(SocketGuildChannel)) return Mediator.Publish(new MessageReceivedNotification(message!), CancellationToken);
        return Task.Run(() => Console.WriteLine(""));
    }

    private Task OnReadyAsync()
    {
        var client = Client;
        return Mediator.Publish(new ReadyNotification(client), CancellationToken);
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
        return Mediator.Publish(new UserJoinNotification(arg, client), CancellationToken);
    }

    private Task OnUserLeftAsync(SocketGuild arg1, SocketUser arg2)
        => Mediator.Publish(new UserLeftNotification(arg1, arg2), CancellationToken);

    private Task OnUserBannedAsync(SocketUser arg1, SocketGuild arg2)
        => Mediator.Publish(new UserBannedNotification(arg1, arg2), CancellationToken);

    private Task OnUserUnBannedAsync(SocketUser arg1, SocketGuild arg2)
        => Mediator.Publish(new UserUnBannedNotification(arg1, arg2), CancellationToken);
}