using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Algorithms
{
    public interface ISetProcessor<TKey>
    {
        bool ProcessSets();
    }
}
