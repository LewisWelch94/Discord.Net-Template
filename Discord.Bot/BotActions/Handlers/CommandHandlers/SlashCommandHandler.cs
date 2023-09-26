namespace Discord.Bot.BotActions.Handlers.CommandHandlers;

using Discord.Bot.BotActions.Notifications.CommandNotifications;
using Discord.Bot.SlashCommands;
using MediatR;

public class SlashCommandHandler : INotificationHandler<SlashCommandNotification>
{
    public async Task Handle(SlashCommandNotification notification, CancellationToken cancellationToken)
    {
        switch (notification.Command.CommandName)
        {
            case "test":
                await new TestSlashCommand().Execute(notification);
                break;

            default:
                throw new ArgumentNullException($"{notification.Command.CommandName} is not a command");
        }
    }
}