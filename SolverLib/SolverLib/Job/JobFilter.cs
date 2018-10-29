using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Space;

namespace SolverLib.Job
{
    public class JobFilter<TKey> : JobBase<TKey>, IJobFilter<TKey>
    {
        public JobFilter(string name, Keys<TKey> keys, IPossible filter) 
            : base(name, JobType.Filter, 1)
        {
            this.Keys = keys;
            this.Filter = filter;
        }

        public IPossible Filter { get; set; }
        
        public Keys<TKey> Keys { get; set; }

        // Get all the possible values from key and filter them using the filter
        public Keys<TKey> Process(IPuzzleEngine<TKey> engine)
        {
            Keys<TKey> keysChanged = engine.Puzzle.Space.Eliminate(Keys, Filter);
            engine.Add(new JobSearch<TKey>("Filter", keysChanged));
            return keysChanged;
        }
    }
}
