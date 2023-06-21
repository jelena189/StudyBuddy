using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudyBuddy.Services.Infrastructure;

namespace StudyBuddy.WebApi
{
    public class NotificationService : BackgroundService
    {
        private readonly ILogger<NotificationService> logger;
        private TimeSpan period;
        private TimeSpan notificationTime;
        private TimeSpan repetitionPeriod;
        private PeriodicTimer timer;

        public NotificationService(ILogger<NotificationService> logger, IOptions<NotificationSettings> notificationSettings)
        {
            this.logger = logger;
            this.period = new TimeSpan(24, 0, 0);
            this.timer = new PeriodicTimer(period);
            this.notificationTime = notificationSettings.Value.NotificationTime;
            this.repetitionPeriod = new TimeSpan(int.Parse(notificationSettings.Value.RepetitionPeriod.Split(':')[0]),
                           int.Parse(notificationSettings.Value.RepetitionPeriod.Split(':')[1]), 0);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var now = DateTime.Now.TimeOfDay;
            logger.LogInformation($"NotificationService Execution Started at: {now.ToString()}");

            period = now < notificationTime ? period = notificationTime.Add(-now) : new TimeSpan(24,0,0) - now.Add(-notificationTime);
            timer = new PeriodicTimer(period);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    logger.LogInformation($"Executed NotificationService - on {DateTime.Now.ToString()}");
                    // send some notifications
                    period = repetitionPeriod;
                    timer = new PeriodicTimer(period);
                }
                catch (Exception ex)
                {
                    logger.LogInformation($"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}

