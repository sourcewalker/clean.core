using Core.Infrastructure.Interfaces.Scheduler;
using Hangfire;

namespace Infrastructure.Hangfire
{
    internal static class HangfireUtility
    {
        internal static string MapjobFrequency(this CronEnum occurence)
        {
            var frequency = Cron.Daily();

            switch (occurence)
            {
                case CronEnum.Daily:
                    frequency = Cron.Daily();
                    break;
                case CronEnum.Minutely:
                    frequency = Cron.Minutely();
                    break;
                case CronEnum.Hourly:
                    frequency = Cron.Hourly();
                    break;
                case CronEnum.Weekly:
                    frequency = Cron.Weekly();
                    break;
                case CronEnum.Monthly:
                    frequency = Cron.Monthly();
                    break;
                case CronEnum.Yearly:
                    frequency = Cron.Yearly();
                    break;
                default:
                    break;
            }

            return frequency;
        }
    }
}
