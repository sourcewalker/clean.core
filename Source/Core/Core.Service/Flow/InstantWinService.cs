using Core.Infrastructure.Interfaces.InstantWin;
using Core.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Service.Flow
{
    public class InstantWinService : IInstantWInService
    {
        private readonly IInstantWinMomentProvider _instantWinProvider;

        public InstantWinService(IInstantWinMomentProvider instantWinProvider)
        {
            _instantWinProvider = instantWinProvider;
        }

        public IList<(Guid Id, string Name)> GenerateInstantWinMoments(GeneratorConfig config, List<Allocable> allocables)
        {
            var instantList = _instantWinProvider.GenerateWinningMoments(config);

            return _instantWinProvider.AllocatePrizes(allocables, instantList.Count);
        }
    }
}
