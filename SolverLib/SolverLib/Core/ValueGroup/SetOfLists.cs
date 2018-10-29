using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Core.ValueGroup
{
    public class SetOfLists<K> : SetOfSets<K>, ISetOfLists<K> where K : ICollection<int>
    {
        
        public SetOfLists() : base(new EnumerableComparer<K, int>())
        {
            
        }

        //public bool ContainsList(IEnumerable<int> group)
        //{
        //    List<int> orderedList = new List<int>(group);
        //    orderedList.Sort(SortDescending);
        //    //orderedList.Reverse();
        //    foreach (K list in this)
        //    {
        //        if (list.SequenceEqual(orderedList))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private int SortDescending(int i1, int i2)
        //{
        //    if (i1 > i2)
        //    {
        //        return -1;
        //    }
        //    if (i1 < i2)
        //    {
        //        return 1;
        //    }
        //    return 0;
        //}

    }
}
