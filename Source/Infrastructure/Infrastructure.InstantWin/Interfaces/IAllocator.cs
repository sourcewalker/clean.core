using Core.Infrastructure.Interfaces.InstantWin;
using System;
using System.Collections.Generic;

namespace Infrastructure.InstantWin.Interfaces
{
    public interface IAllocator
    {
        IList<(Guid Id, string Name)> Allocate(IList<Allocable> allocable, int instantWinNumber);
    }
}
