using Core.Infrastructure.Interfaces.InstantWin;
using Infrastructure.InstantWin.Enums;
using System;

namespace Infrastructure.InstantWin.Configuration
{
    public struct ProviderConfiguration
    {
        public struct Campaign
        {
            public static DateTimeOffset StartDate = new DateTimeOffset(2019, 07, 29, 0, 0, 0, TimeSpan.FromHours(1));

            public static DateTimeOffset EndDate = new DateTimeOffset(2019, 09, 01, 23, 59, 59, TimeSpan.FromHours(1));

            // DateTime just for storing the time, date information is not relevant
            public static DateTimeOffset OpenTime = new DateTimeOffset(2019, 07, 29, 8, 0, 0, TimeSpan.FromHours(1));

            // DateTime just for storing the time, date information is not relevant
            public static DateTimeOffset CloseTime = new DateTimeOffset(2019, 07, 29, 21, 0, 0, TimeSpan.FromHours(1));
        }

        public struct Generator
        {
            public static int LimitNumber = 3;

            public static GeneratorLimitOptions limitOption = GeneratorLimitOptions.LimitPerHour;

            public static GeneratorAlgorithms algorithm = GeneratorAlgorithms.DivideAndConquer;
        }

        public struct Allocator
        {
            public static AllocatorAlgorithms algorithm = AllocatorAlgorithms.Fair;
        }
    }
}
