using Grpc.Core;
using GrpcServer;
using Telegram.Bot;

namespace Server.Services;

public class NotifyService : Notify.NotifyBase
{
    public readonly ITelegramBotClient _BotClient;
    public NotifyService(ITelegramBotClient botClient)
    {
        _BotClient = botClient;
    }
    public override Task<NotificationResponse> SendNotification(NotificationMessage request, ServerCallContext context)
    {

        string theMessage = $"Qovun's Server\nMessage:{request.Text}\nSender:{request.SenderName}\nText:{request.Text}";
        _BotClient.SendTextMessageAsync(" 752472151", theMessage);
        return Task.FromResult(new NotificationResponse()
        {
            IsSuccess = !string.IsNullOrEmpty(request.ChatId),
            SentMessage = "Ake shaftoli bomi?"
        });
    }
}
