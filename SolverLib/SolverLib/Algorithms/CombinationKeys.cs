using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Algorithms
{
    public class CombinationKeys<TKey> : ISetProcessor<TKey>
    {
        private Keys<TKey> set;
        private int elements;
        private ISetTester<TKey> setTester;

        /// <summary>
        /// Create this algorithm
        /// </summary>
        /// <param name="setIn">The set used for combinations</param>
        /// <param name="elementsIn">The number of elements to combine</param>
        /// /// <param name="setTesterIn">The test to see if this combination should be remembered</param>
        public CombinationKeys(Keys<TKey> set, int elements, ISetTester<TKey> setTester)
        {
            this.set = set;
            this.elements = elements;
            this.setTester = setTester;
        }

        // e.g. 3 in 9 = first iterator 0 to (9-3) = 6
        /// <summary>
        /// Use the visitor to do something with each combination found
        /// </summary>
        /// <returns></returns>
        public bool ProcessSets()
        {
            Keys<TKey>.Enumerator startEnumerator = this.set.GetEnumerator();
            startEnumerator.MoveNext();
            Keys<TKey> buildSet = new Keys<TKey>();
            return this.AddSetCombinations(startEnumerator, 0, 0, buildSet);
        }

        private bool AddSetCombinations(
            Keys<TKey>.Enumerator startEnumerator,
            int currentElement,
            int offset,
            Keys<TKey> currentCombination)
        {
            bool found = false;
            for (int loop = offset + currentElement; !found && loop < set.Count - elements + currentElement + 1; loop++)
            {
                // Add key
                currentCombination.Add(startEnumerator.Current);
                // need another element?
                if (currentElement + 1 < this.elements)
                {
                    Keys<TKey>.Enumerator nextEnumerator = startEnumerator;
                    nextEnumerator.MoveNext();
                    found = this.AddSetCombinations(nextEnumerator, currentElement + 1, offset, currentCombination);
                }
                else
                {
                    found = this.setTester.Test(currentCombination, this.set);
                }
                currentCombination.Remove(startEnumerator.Current);
                startEnumerator.MoveNext();
                offset += 1;
            }
            return found;
        }
    }
}
