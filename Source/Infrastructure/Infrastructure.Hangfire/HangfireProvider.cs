using Core.Infrastructure.Interfaces.CRM;
using Core.Infrastructure.Interfaces.Scheduler;
using Core.Shared.Models;
using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Hangfire
{
    public class HangfireProvider : ISchedulerProvider
    {

        public async Task DelayedParticipationRetrySync(
            CrmData data,
            Configurations requestWideSettings,
            TimeSpan delay,
            bool requestConsumerId = false)
        {
            await Task.Run(() =>
                    BackgroundJob.Schedule<ICrmConsumerProvider>(
                        iCrmProvider =>
                             iCrmProvider.CreateParticipationAsync(
                                 data,
                                 requestWideSettings,
                                 requestConsumerId),
                             delay));
        }

        public async Task<object> RetryParticipationSyncImmediately(
            CrmData data,
            Configurations requestWideSettings,
            bool requestConsumerId = false)
        {
            var jobId = await Task.Run(() => BackgroundJob.Enqueue<ICrmConsumerProvider>(
                            iCrmProvider =>
                                iCrmProvider.CreateParticipationAsync(
                                    data,
                                    requestWideSettings,
                                    requestConsumerId)));

            var result = new object();
            IMonitoringApi monitoringApi = JobStorage.Current.GetMonitoringApi();
            JobDetailsDto jobDetails = monitoringApi.JobDetails(jobId);
            SucceededJobDto jobDto = monitoringApi.SucceededJobs(0, int.MaxValue)
                                                    .First()
                                                    //.FirstOrDefault(job => job.Key == "Key")
                                                    .Value;
            if (jobDto != null)
            {
                result = jobDto.Result;
                return JsonConvert.DeserializeObject<CrmResponse>(result.ToString());
            }

            return null;
        }

        public async Task RetryParticipationSyncRecurrently(
            CrmData data,
            Configurations requestWideSettings,
            CronEnum occurence,
            bool requestConsumerId = false)
        {
            var frequency = occurence.MapjobFrequency();

            await Task.Run(() => RecurringJob.AddOrUpdate<ICrmConsumerProvider>(
                                    iCrmProvider =>
                                        iCrmProvider.CreateParticipationAsync(
                                            data,
                                            requestWideSettings,
                                            requestConsumerId),
                                        frequency));
        }
    }
}
