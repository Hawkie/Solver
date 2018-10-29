using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public abstract class LogicNode : List<ILogicNode>, ILogicNode
    {
        public ILogicNode Add(ILogicLeaf leaf)
        {
            ILogicNode node = new LogicNodeLeaf(leaf);
            this.Add(node);
            return node;
        }

        public virtual void Parse(object data, ILogicStack stack)
        {
            foreach (ILogicNode node in this)
            {
                node.Parse(data, stack);
            }
        }

    }
}
