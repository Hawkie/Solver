using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Job;
using SolverLib.Puzzle;
using SolverLib.Space;

namespace SolverLib.Engine
{
    public interface IPuzzleEngine<TKey> : ICollection<IJob<TKey>>
    {
        IPuzzle<TKey> Puzzle { get; set; }

        event SolverFinishedHandler SolverFinishedEvent;

        void SetInitialValues(ISpace<TKey> initialValues);

        void RunActionCount(int jobCount);

        void Solve(bool async);

        Keys<TKey> DoNextJob();

        IJob<TKey> Peek();
    }
}
