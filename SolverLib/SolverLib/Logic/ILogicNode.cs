using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public interface ILogicNode : IList<ILogicNode>
    {
        ILogicNode Add(ILogicLeaf leaf);

        //void EvaluateLeaves(object data);

        void Parse(object data, ILogicStack stack);
    }
}
