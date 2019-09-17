using Core.Infrastructure.Interfaces.InstantWin;
using Infrastructure.InstantWin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.InstantWin.Allocator.Algorithms
{
    public class BlindAlgorithm : IAllocator
    {
        public IList<(Guid Id, string Name)> Allocate(IList<Allocable> allocable, int instantWinNumber)
        {
            var allocableList = new List<(Guid, string)>();
            var random = new Random();

            for (var i = 0; i < instantWinNumber; i++)
            {
                allocable = allocable.Where(p => p.Number > 0)
                               .ToList();

                var ranIndex = random.Next(allocable.Count);

                if (allocable[ranIndex].Number > 0)
                {
                    allocableList.Add((allocable[ranIndex].Id, allocable[ranIndex].Name));
                    allocable[ranIndex].Number -= 1;
                }
            }

            return allocableList;
        }
    }
}
