using Telegram.Bot;

namespace BadBoyNeedsAlittleLove.Entities
{
    public class WorkNigerWork : BackgroundService
    {

        private readonly ILogger<WorkNigerWork> _logger;
        public static readonly TelegramBotClient _telegramBotClient = new("bot_token");

        public WorkNigerWork(ILogger<WorkNigerWork> logger)
        {
            _logger = logger;
            _telegramBotClient.StartReceiving<TelegramService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);
                _logger.LogInformation("Niggers started working at: {time}", DateTimeOffset.Now);
            }
        }


    }
}
