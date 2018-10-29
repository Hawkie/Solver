using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;

namespace SolverLib.Job
{
    public interface IJob<TKey>
    {
        string Name { get; }

        JobType Type { get; }

        int Priority { get; set; }

        Keys<TKey> Process(IPuzzleEngine<TKey> engine);
    }
}
