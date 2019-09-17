using Infrastructure.InstantWin.Enums;
using Infrastructure.InstantWin.Generator.Algorithms;
using Infrastructure.InstantWin.Interfaces;

namespace Infrastructure.InstantWin.Generator.Factory
{
    public static class GeneratorFactory
    {
        public static IGenerator Create(GeneratorAlgorithms algorithm)
        {
            switch (algorithm)
            {
                case GeneratorAlgorithms.DivideAndConquer:
                default:
                    return new DivideAndConquerAlgorithm();
            }
        }
    }
}
