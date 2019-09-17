using Core.Infrastructure.Interfaces.InstantWin;
using Infrastructure.InstantWin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.InstantWin.Generator.Algorithms
{
    public class DivideAndConquerAlgorithm : IGenerator
    {
        public IList<DateTimeOffset> Generate(GeneratorConfig config)
        {
            var startDate = config.StartDate;
            var endDate = config.EndDate;
            var openHour = config.OpenTime;
            var closeHour = config.CloseTime;
            var limitOptions = config.LimitOption;

            // Dividing Timespan between start and end date into multiple intervals
            // According to limit option
            var limitPerInterval = config.LimitNumber;
            var intervalLength = GetDivisionInterval(limitOptions, startDate, endDate);

            var firstIntervalDate = startDate;

            if (firstIntervalDate.TimeOfDay < openHour.TimeOfDay)
            {
                firstIntervalDate = firstIntervalDate.Date + openHour.TimeOfDay;
            }

            if (closeHour.TimeOfDay < firstIntervalDate.TimeOfDay)
            {
                firstIntervalDate = firstIntervalDate.AddDays(1).Date + openHour.TimeOfDay;
            }

            var lastIntervalDate = endDate <= (firstIntervalDate + intervalLength) ?
                                    endDate :
                                    (firstIntervalDate + intervalLength);

            var dateList = new List<DateTimeOffset>();
            var random = new Random();
            byte[] bytes = new byte[8];
            var randomAddition = new Random();

            do
            {
                for (var i = 1; i <= limitPerInterval; i++)
                {
                    random.NextBytes(bytes);
                    var ranDouble = randomAddition.NextDouble();
                    var randomDate = GenerateRandomDateBetweenInterval(
                                        bytes,
                                        firstIntervalDate,
                                        lastIntervalDate,
                                        openHour,
                                        closeHour);
                    var differentElement = EnsureDifferentDate(
                                            ranDouble,
                                            randomDate,
                                            dateList,
                                            firstIntervalDate,
                                            lastIntervalDate);
                    dateList.Add(differentElement);
                }

                var nextInterval = SwitchToNextInterval(
                                    limitOptions,
                                    lastIntervalDate,
                                    intervalLength,
                                    endDate,
                                    openHour,
                                    closeHour);
                firstIntervalDate = nextInterval.nextIntervalStart;
                lastIntervalDate = nextInterval.nextIntervalEnd;
            }
            while (lastIntervalDate < endDate);

            return dateList;
        }

        private TimeSpan GetDivisionInterval(GeneratorLimitOptions limitOptions,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            switch (limitOptions)
            {
                case GeneratorLimitOptions.LimitPerHour:
                    return TimeSpan.FromHours(1);
                case GeneratorLimitOptions.LimitPerDay:
                    return TimeSpan.FromDays(1);
                case GeneratorLimitOptions.LimitPerMonth:
                    return TimeSpan.FromDays(30);
                case GeneratorLimitOptions.LimitPerCampaign:
                default:
                    return endDate - startDate;
            }
        }

        private (DateTimeOffset nextIntervalStart, DateTimeOffset nextIntervalEnd) SwitchToNextInterval(
            GeneratorLimitOptions limitOption,
            DateTimeOffset previousIntervalEnd,
            TimeSpan intervalLength,
            DateTimeOffset EndLimit,
            DateTimeOffset openHour,
            DateTimeOffset closeHour)
        {
            var nextIntervalStart = previousIntervalEnd;

            if (limitOption == GeneratorLimitOptions.LimitPerHour)
            {
                if (nextIntervalStart.TimeOfDay < openHour.TimeOfDay)
                {
                    nextIntervalStart = nextIntervalStart.Date + openHour.TimeOfDay;
                }

                if (closeHour.TimeOfDay <= nextIntervalStart.TimeOfDay)
                {
                    nextIntervalStart = nextIntervalStart.AddDays(1).Date + openHour.TimeOfDay;
                }
            }

            var nextIntervalEnd = EndLimit <= (nextIntervalStart + intervalLength) ?
                                    EndLimit :
                                    (nextIntervalStart + intervalLength);

            if (nextIntervalStart > nextIntervalEnd)
            {
                nextIntervalEnd = nextIntervalStart + intervalLength;
            }

            return (nextIntervalStart, nextIntervalEnd);
        }

        private DateTimeOffset GenerateRandomDateBetweenInterval(
            byte[] bytes,
            DateTimeOffset firstDate,
            DateTimeOffset lastDate,
            DateTimeOffset openHour,
            DateTimeOffset closeHour)
        {
            var timeSpan = lastDate - firstDate;
            var additionSpan = GetRandomTimeInTimeSpan(bytes, timeSpan);
            var newRandom = firstDate + additionSpan;

            // Ensure Interval time is between open and close hours
            if (newRandom.TimeOfDay < openHour.TimeOfDay || closeHour.TimeOfDay < newRandom.TimeOfDay)
            {
                var openHourInterval = closeHour.TimeOfDay - openHour.TimeOfDay;
                var addSpan = GetRandomTimeInTimeSpan(bytes, openHourInterval);
                var randomTime = openHour.TimeOfDay + addSpan;
                newRandom = newRandom.Date + randomTime;
            }

            return newRandom;
        }

        private TimeSpan GetRandomTimeInTimeSpan(byte[] bytes, TimeSpan timeSpan)
        {
            var ranInt64 = Math.Abs(BitConverter.ToInt64(bytes, 0)) % timeSpan.Ticks;
            return new TimeSpan(ranInt64);
        }

        private DateTimeOffset EnsureDifferentDate(
            double ranDouble,
            DateTimeOffset currentDatetime,
            IEnumerable<DateTimeOffset> dateList,
            DateTimeOffset firstDate,
            DateTimeOffset lastDate)
        {
            if (!dateList.Contains(currentDatetime))
            {
                return currentDatetime;
            }
            else
            {
                do
                {
                    if (ranDouble <= 0.5)
                    {
                        currentDatetime -= TimeSpan.FromSeconds(ranDouble * 10);
                    }
                    else
                    {
                        currentDatetime += TimeSpan.FromSeconds(ranDouble * 10);
                    }
                }
                while (dateList.Contains(currentDatetime) ||
                        currentDatetime < firstDate ||
                        lastDate < currentDatetime);
                return currentDatetime;
            }
        }
    }
}
