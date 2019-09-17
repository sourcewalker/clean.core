using Core.Infrastructure.Interfaces.InstantWin;
using Infrastructure.InstantWin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.InstantWin.Allocator.Algorithms
{
    public class FairAlgorithm : IAllocator
    {
        public IList<(Guid Id, string Name)> Allocate(IList<Allocable> allocable, int instantWinNumber)
        {
            var allocableList = Enumerable.Repeat((default(Guid), string.Empty), instantWinNumber).ToList();

            allocable = allocable.OrderBy(p => p.Number).ToList();

            var random = new Random();

            for (var allocableTypeIndex = 0; allocableTypeIndex < allocable.Count; allocableTypeIndex++)
            {
                for (var samePrizeIndex = 0; samePrizeIndex < allocable[allocableTypeIndex].Number; samePrizeIndex++)
                {
                    var ranIndex = random.Next(instantWinNumber);
                    if (allocableList.ElementAtOrDefault(ranIndex) != (default(Guid), string.Empty))
                    {
                        ranIndex = GetAvailableNearByIndex(allocableList, ranIndex);
                    }
                    allocableList[ranIndex] = (allocable[allocableTypeIndex].Id, allocable[allocableTypeIndex].Name);
                }
            }

            return allocableList;
        }

        private int GetAvailableNearByIndex(IList<(Guid Id, string Name)> list, int randomIndex)
        {
            return list
                    .Select(
                      (value, index) =>
                        new{
                            Index = index,
                            Value = value,
                            Distance = Math.Abs(index - randomIndex)
                        })
                    .Where(
                      element => element.Value == (default(Guid), string.Empty))
                    .Aggregate(
                      (firstElement, secondElement) =>
                        firstElement.Distance < secondElement.Distance ?
                            firstElement : secondElement)
                    .Index;
        }
    }
}
