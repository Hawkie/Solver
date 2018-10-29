using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;

namespace SolverLib.Job
{
    public class JobSearch<TKey> : JobBase<TKey>, IJobSearch<TKey>
    {
        public JobSearch(string name, Keys<TKey> keysChanged)
            : base(name, JobType.None, 2)
        {
            this.KeysChanged = keysChanged;
        }

        public Keys<TKey> KeysChanged { get; set; }

        public virtual Keys<TKey> Process(IPuzzleEngine<TKey> engine)
        {
            engine.Puzzle.Constraints.CreateSearchJobs(KeysChanged, engine);
            return new Keys<TKey>();
        }
    }
}
