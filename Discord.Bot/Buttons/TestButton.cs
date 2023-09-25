namespace Discord.Bot.Buttons;

using Discord;
using Discord.Bot.BotActions.Interfaces;
using Discord.Bot.BotActions.Notifications;

public class TestButton : IButton
{
    public ButtonBuilder Createbutton()
    {
        return new ButtonBuilder()
        {
            CustomId = "test",
            Label = "test",
            Style = ButtonStyle.Primary
        };
    }

    public async Task Execute(ButtonNotification notification)
    {
        await notification.Button.RespondAsync("Button worked");
    }
}