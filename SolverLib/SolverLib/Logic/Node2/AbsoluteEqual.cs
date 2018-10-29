using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic.Node2
{
    public class AbsoluteEqual : LogicNode2
    {
        // Add more constructors later
        public AbsoluteEqual(ILogicNode actual, ILogicLeaf expected, int penalty)
        {
            this.Add(actual);
            this.Add(new LogicNodeLeaf(expected));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(penalty))));
        }

        // Add more constructors later
        public AbsoluteEqual(ILogicLeaf actual, ILogicLeaf expected, ILogicLeaf penalty)
        {
            this.Add(new LogicNodeLeaf(actual));
            this.Add(new LogicNodeLeaf(expected));
            this.Add(new LogicNodeLeaf(penalty));
        }

        // Add more constructors later
        public AbsoluteEqual(ILogicLeaf actual, ILogicLeaf expected, int penalty)
        {
            this.Add(new LogicNodeLeaf(actual));
            this.Add(new LogicNodeLeaf(expected));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(penalty))));
        }

        public AbsoluteEqual(ILogicLeaf leafL, int expected, int penalty)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(expected))));
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

            ILogicOperation op = new LogicOperation("AbsoluteEqual");
            op.Add(v3.Key);
            op.Add(v2.Key);
            op.Add(v1.Key);
            ILogicResult notequal = new LogicResult(0);
            
            if (v1.Value.CompareTo(v2.Value) != 0)
            {
                notequal.Value = v3.Value.ToInt32;
            }

            op.Result = notequal.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, notequal));
        }
    }
}