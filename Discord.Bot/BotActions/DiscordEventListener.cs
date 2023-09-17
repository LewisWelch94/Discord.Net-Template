namespace Discord.Bot.BotActions;

using Discord.Bot.BotActions.Notifications;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class DiscordEventListener
{
    private readonly CancellationToken _cancellationToken;
    private readonly DiscordSocketClient Client;
    private readonly IServiceScopeFactory _serviceScope;

    public DiscordEventListener(DiscordSocketClient client, IServiceScopeFactory serviceScope)
    {
        Client = client;
        _serviceScope = serviceScope;
        _cancellationToken = new CancellationTokenSource().Token;
    }

    private IMediator Mediator
    {
        get
        {
            var scope = _serviceScope.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IMediator>();
        }
    }

    public Task StartAsync()
    {
        Client.Ready += OnReadyAsync;
        Client.MessageReceived += OnMessageReceivedAsync;
        Client.SlashCommandExecuted += OnSlashCommandExecutedAsync;

        return Task.CompletedTask;
    }

    private Task OnMessageReceivedAsync(SocketMessage arg)
    {
        var message = arg as SocketUserMessage;
        return Mediator.Publish(new MessageReceivedNotification(message!), _cancellationToken);
    }

    private Task OnReadyAsync()
    {
        var client = Client;
        return Mediator.Publish(new ReadyNotification(client), _cancellationToken);
    }

    private Task OnSlashCommandExecutedAsync(SocketSlashCommand arg)
    => Mediator.Publish(new SlashCommandNotification(arg), _cancellationToken);
}