using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Core.ValueGroup
{
    public interface ISetOfSets<K> : ICollection<K> where K : ICollection<int>
    {
        //IPossibleProperties<K, int> CategoryCount(ICollection<K> qualifiers);

        //// MinMaxSum
        //MinMaxValue MinMaxSum(IPossibleProperties<K, int> qualifyAndPoints);

        //// Categorised
        //SortedList<K, IList<int>> Categorised(IPossibleProperties<K, int> qualifyAndPoints);

        //IPossibleProperties<K, int> Categorised(IPossibleProperties<K, int> qualifyAndPoints,
        //                                        Func<ICollection<int>, int> func);

        //int Categorised(IPossibleProperties<K, int> qualifyAndPoints, Func<ICollection<int>, int> func,
        //                Func<ICollection<int>, int> func2);
        //// 
        
        //bool DoesNotContainAny(ISetOfSets<K> group);

        //bool ContainsAll(ISetOfSets<K> group);

    }
}
