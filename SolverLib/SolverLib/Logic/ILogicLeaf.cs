using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public interface ILogicLeaf
    {
        ILogicResult Value { get; set; }

        KeyValuePair<ILogicOperation, ILogicResult> Check(object data);
        
    }
}
