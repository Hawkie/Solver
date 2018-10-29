using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicNodeLeaf : LogicNode
    {
        public LogicNodeLeaf(ILogicLeaf leaf)
        {
            this.Leaf = leaf;
        }

        public LogicNodeLeaf(KeyValuePair<ILogicOperation, ILogicResult> result)
        {
            this.Leaf = new LogicLeaf(result.Key.Name, result.Value);
        }

        ILogicLeaf Leaf { get; set; }

        public override void Parse(object data, ILogicStack stack)
        {
            stack.Push(this.Leaf.Check(data));
        }
    }
}
