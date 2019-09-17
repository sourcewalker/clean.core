using Core.Infrastructure.Interfaces.InstantWin;
using Infrastructure.InstantWin.Allocator.Factory;
using Infrastructure.InstantWin.Configuration;
using Infrastructure.InstantWin.Generator.Factory;
using System;
using System.Collections.Generic;

namespace Infrastructure.InstantWin.Provider
{
    public class InstantWinProvider : IInstantWinMomentProvider
    {
        public IList<DateTimeOffset> GenerateWinningMoments(GeneratorConfig config)
        {
            var generator = GeneratorFactory.Create(ProviderConfiguration.Generator.algorithm);
            return generator.Generate(config);
        }

        public IList<(Guid Id, string Name)> AllocatePrizes(IList<Allocable> allocable, int instantWinNumber)
        {
            var allocator = AllocatorFactory.Create(ProviderConfiguration.Allocator.algorithm);
            return allocator.Allocate(allocable, instantWinNumber);
        }
    }
}
