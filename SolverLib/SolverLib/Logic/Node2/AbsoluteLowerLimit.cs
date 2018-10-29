using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic.Node2
{
    public class AbsoluteLowerLimit : LogicNode2
    {
        // Add more constructors later
        public AbsoluteLowerLimit(ILogicLeaf leafL, ILogicLeaf leafR, int penalty)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(leafR));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(penalty))));
        }

        public AbsoluteLowerLimit(ILogicLeaf leafL, int limit, int penalty)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(limit))));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(penalty))));
        }

        /// <summary>
        /// The min, max, add, sub nodes all expect two nodes that have one result on its leaf
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override void Parse(object data, ILogicStack stack)
        {
            base.Parse(data, stack);

            KeyValuePair<ILogicOperation, ILogicResult> v3 = stack.Pop();
            KeyValuePair<ILogicOperation, ILogicResult> v2 = stack.Pop();
            KeyValuePair<ILogicOperation, ILogicResult> v1 = stack.Pop();

            ILogicOperation op = new LogicOperation("AbsoluteLower");
            op.Add(v3.Key);
            op.Add(v2.Key);
            op.Add(v1.Key);
            ILogicResult below = new LogicResult(0);
            
            if (v1.Value.CompareTo(v2.Value) == -1)
            {
                below = new LogicResult(v3.Value.ToInt32);
            }

            op.Result = below.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, below));
        }
    }
}