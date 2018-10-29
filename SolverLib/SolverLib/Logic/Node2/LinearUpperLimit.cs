using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic.Node2
{
    public class LinearUpperLimit : LogicNode2
    {
        // Add more constructors later
        public LinearUpperLimit(ILogicLeaf leafL, ILogicLeaf leafR)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(leafR));
        }

        public LinearUpperLimit(ILogicNode node, int limit)
        {
            this.Add(node);
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(limit))));
        }

        public LinearUpperLimit(ILogicLeaf leafL, int limit)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(limit))));
        }

        /// <summary>
        /// The min, max, add, sub nodes all expect two nodes that have one result on its leaf
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override void Parse(object data, ILogicStack stack)
        {
            base.Parse(data, stack);
            KeyValuePair<ILogicOperation, ILogicResult> v2 = stack.Pop();
            KeyValuePair<ILogicOperation, ILogicResult> v1 = stack.Pop();

            ILogicOperation op = new LogicOperation("LinearUpper");
            op.Add(v2.Key);
            op.Add(v1.Key);
            ILogicResult above = new LogicResult(0);
            if (v1.Value.CompareTo(v2.Value) == 1)
            {
                above = v1.Value.Subtract(v2.Value);
            }

            op.Result = above.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, above));
        }
    }
}