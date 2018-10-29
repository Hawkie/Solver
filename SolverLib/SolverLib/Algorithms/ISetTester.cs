using SolverLib.Core;

namespace SolverLib.Algorithms
{
    public interface ISetTester<TKey>
    {
        bool Test<TKey>(Keys<TKey> currentCombination, Keys<TKey> set);
    }
}