using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Algorithms;
using SolverLib.Core;
using SolverLib.Space;

namespace SolverLib.SetTesters
{
    public class CompleteSetTester<TKey>
    {
        public CompleteSetTester(ISpace<TKey> space)
        {
            this.Space = space;
        }

        public CompleteSetTester(TKey kSource, ISpace<TKey> space)
        {
            this.SourceKey = kSource;
            this.Space = space;
            }


        public ISpace<TKey> Space { get; set;}
        public TKey SourceKey { get; set; }
        
        /// <summary>
        ///  If combined values are equal to the spaces then its a complete set
        /// e.g. 1-3,1-3,1-3,1-9,1-9,1-9,1-9,1-9,1-9 (Keys1,2,3 are complete)
        /// e.g. 1-2,2-3,1:3,1-9,1-9,1-9,1-9,1-9,1-9 (Keys1,2,3 are complete)
        /// e.g. 1-9,1-9,1-9,4-9,4-9,4-9,4-9,4-9,4-9 (Keys4-9 are complete)
        /// </summary>
        public bool CompleteSet(IList<TKey> set)
        {
            IPossible allValues = Space.AllValuesAt(set);
            if (allValues.Values.Count == set.Count)
            {
                return true;
            }
            return false;
        }

        public bool CompleteSet2(IList<TKey> set)
        {
            set.Add(this.SourceKey);
            IPossible allValues = Space.AllValuesAt(set);
            if (allValues.Values.Count == set.Count)
            {
                return true;
            }
            return false;
        }
    }   
}
