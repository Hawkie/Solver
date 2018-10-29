using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.SetTesters;

namespace SolverLib.Algorithms
{
    public class CombinationValues<TKey>
    {
        public IList<TKey> Values { get; set; }
        public int Size { get; set;}
        public bool FindOne { get; set; }
        public bool Store { get; set; }
        public int Offset { get; set; }
        public int Count { get; private set; }
        public ICollection<IList<TKey>> Combinations { get; private set; }
        public TestFunction Test;

        public delegate bool TestFunction(IList<TKey> set);

        public CombinationValues(int size, ICollection<TKey> values)
        {
            this.FindOne = false;
            this.Store = true;
            this.Count = 0;
            this.Offset = 1;
            this.Values = values.ToArray();
            this.Size = size;
            this.Test = AlwaysTrue;
            this.Combinations = new List<IList<TKey>>();
        }

        public bool AlwaysTrue(IList<TKey> set)
        {
            return true;
        }

        /// <summary>
        /// Add the combinations that pass the test to the visitor
        /// </summary>
        public int CalcCombinations()
        {
            IList<TKey> set = new List<TKey>();
            if (Size > 0)
            {
                BuildSet(0, set);
            }
            return this.Count;
        }

        private void BuildSet(int index, IList<TKey> set)
        {
            // Can make this more efficient so it ends more abruptly
            while (index < this.Values.Count - (this.Size - set.Count - 1))
            {
                TKey current = Values[index];
                IList<TKey> newSet = new List<TKey>(set);
                newSet.Add(current);
                if (newSet.Count < this.Size)
                {
                    this.BuildSet(index+this.Offset, newSet);
                }
                else
                {
                    if (Test(newSet))
                    {
                        this.Count++;
                        if (this.Store)
                        {
                            this.Combinations.Add(newSet);
                        }
                        if (this.FindOne)
                        {
                            return;
                        }
                    }
                }
                index++;
            }
        }
    }
}
