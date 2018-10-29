using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Engine;

namespace SolverLib.Job
{
    public interface IJobSearch<TKey> : IJob<TKey>
    {
        Keys<TKey> KeysChanged { get; set; }

    }
}
