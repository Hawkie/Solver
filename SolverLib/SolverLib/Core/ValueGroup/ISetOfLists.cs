using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Core.ValueGroup
{
    public interface ISetOfLists<K> : ISetOfSets<K> where K : ICollection<int>
    {
        //bool ContainsList(IEnumerable<int> list);
    }
}
