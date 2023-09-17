﻿namespace Discord.Bot.BotActions.Notifications;

using Discord.WebSocket;
using MediatR;

public class MessageReceivedNotification : INotification
{
    public SocketMessage Message { get; }

    public MessageReceivedNotification(SocketMessage message)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }
}