using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Job
{
    public interface IJobFilter<TKey> : IJob<TKey>
    {
        IPossible Filter { get; set; }
        Keys<TKey> Keys { get; set; }
    }
}
