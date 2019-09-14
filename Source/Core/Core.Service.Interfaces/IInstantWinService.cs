using Core.Infrastructure.Interfaces.InstantWin;
using System;
using System.Collections.Generic;

namespace Core.Service.Interfaces
{
    public interface IInstantWInService
    {
        IList<(Guid Id, string Name)> GenerateInstantWinMoments(GeneratorConfig config, List<Allocable> allocables);
    }
}
