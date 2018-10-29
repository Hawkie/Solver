using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic.Node2
{
    public class LinearEqual : LogicNode2
    {
        // Add more constructors later
        public LinearEqual(ILogicLeaf leafL, ILogicLeaf leafR)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(leafR));
        }

        public LinearEqual(ILogicNode nodeL, ILogicLeaf leafR)
        {
            this.Add(nodeL);
            this.Add(new LogicNodeLeaf(leafR));
        }

        public LinearEqual(ILogicLeaf leafL, int limit)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(limit))));
        }

        public LinearEqual(ILogicNode node, int limit)
        {
            this.Add(node);
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
  
            ILogicResult result = new LogicResult(0);
            ILogicOperation op = new LogicOperation("LinearEqual");
            op.Add(v2.Key);
            op.Add(v1.Key);
            if (v1.Value.CompareTo(v2.Value) != 0)
            {
                result = new LogicResult(v2.Value.ToInt32 - v1.Value.ToInt32);
            }

            op.Result = result.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, result));
        }
    }
}