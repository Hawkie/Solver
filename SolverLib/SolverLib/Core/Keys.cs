using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Space;

namespace SolverLib.Core
{
    public class Keys<TKey> : HashSet<TKey>, IEnumerable<TKey>
    {
        public Keys()
        {

        }


        public Keys(IEnumerable<TKey> that)
        {
            this.UnionWith(that);
        }

        /// <summary>
        /// This method is looking for a subset that contains values that
        /// the outer set does not contain
        /// </summary>
        /// <param name="outerSet"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public IPossible ReducedSetValues(IEnumerable<TKey> outerSet, ISpace<TKey> space)
        {
            Keys<TKey> oSet = new Keys<TKey>(outerSet);
            oSet.ExceptWith(this);
            IPossible reducedValues = space.AllValuesAt(this);
            // Get all values in subset combination
            foreach (TKey keyInner in this)
            {
                Possible innerValues = new Possible(space[keyInner]);
                foreach (TKey keyOuter in oSet)
                {
                    innerValues.FilterOut(space[keyOuter]);
                }    
                // Each innerKey must have an exclusive value not in the outer set
                if (innerValues.Count > 0)
                {
                    reducedValues.IntersectPossible(innerValues);
                }
                else
                {
                    reducedValues.Clear();
                    break;
                }
            }
            return reducedValues;
        }

        public Possible UnionValues(ISpace<TKey> space)
        {
            Possible testUnion = new Possible();
            foreach (TKey key in this)
            {
                testUnion.UnionPossible(space[key]);
            }
            return testUnion;
        }
    }
}
