using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Algorithms;
using SolverLib.Core;
using SolverLib.Space;

namespace SolverLib.SetTesters
{
    public class ReducedSetTester<TKey>
    {
        public ISpace<TKey> Space { get; private set; }

        public Keys<TKey> OuterSet { get; private set;}

        public ICollection<IRegion<TKey>> Regions { get; set; }

        public ReducedSetTester(Keys<TKey> outerSet, ISpace<TKey> space)
        {
            this.Space = space;
            this.OuterSet = outerSet;
            this.Regions = new List<IRegion<TKey>>();
        }

        /// <summary>
        /// This method is looking for a subset that contains values that
        /// the outer set does not contain.
        /// E.g. 1-9,2-9,2-9,2-9,2-9,2-9,2-9,2-9,2-9 (Only Key1 is a reduced set9)
        /// or   1-9,1-9,1-9,2-9,2-9,2-9,2-9,2-9,2-9 (Only Keys1-3 contain 1)
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public bool ReducedSet(IEnumerable<TKey> set)
        {
            Keys<TKey> inner = new Keys<TKey>(set);
            IPossible allValues = inner.ReducedSetValues(OuterSet, Space);
            if (allValues.Count > 0)
            {
                IRegion<TKey> reduced = new Region<TKey>(set, allValues);
                this.Regions.Add(reduced);
                return true;
            }
            return false;
        }
    }
}
