﻿namespace Discord.Bot.BotActions.Notifications;

using Discord.WebSocket;
using MediatR;

public class SlashCommandNotification : INotification
{
    public SocketSlashCommand Command { get; }

    public SlashCommandNotification(SocketSlashCommand command)
    {
        Command = command ?? throw new ArgumentNullException(nameof(command));
    }
}