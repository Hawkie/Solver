using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public interface ILogicStack : IEnumerable< KeyValuePair<ILogicOperation, ILogicResult> >
    {
        void Push(KeyValuePair<ILogicOperation, ILogicResult> item);
        KeyValuePair<ILogicOperation, ILogicResult> Pop();
        KeyValuePair<ILogicOperation, ILogicResult> Peek();
    }
}
