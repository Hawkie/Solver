using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicStack : Stack<KeyValuePair<ILogicOperation, ILogicResult>>, ILogicStack
    {
        
    }
}
