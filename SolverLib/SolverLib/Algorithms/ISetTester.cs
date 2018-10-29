using SolverLib.Core;

namespace SolverLib.Algorithms
{
    public interface ISetTester<TKey>
    {
        bool Test(Keys<TKey> currentCombination, Keys<TKey> set);
    }
}