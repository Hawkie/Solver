using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic.Node2
{
    public class LogicBitwiseOrTrue : LogicNode2
    {
        // Add more constructors later
        public LogicBitwiseOrTrue(ILogicLeaf leafL, ILogicLeaf leafR)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(leafR));
        }

        public LogicBitwiseOrTrue(ILogicNode nodeL, ILogicLeaf leafR)
        {
            this.Add(nodeL);
            this.Add(new LogicNodeLeaf(leafR));
        }

        public LogicBitwiseOrTrue(ILogicLeaf leafL, int limit)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(limit))));
        }

        public LogicBitwiseOrTrue(ILogicNode node, int limit)
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

            ILogicOperation op = new LogicOperation("BitwiseOrTrue");
            op.Add(v2.Key);
            op.Add(v1.Key);
            ILogicResult result = new LogicResult(LogicResult.MinValue);
            
            if (v1.Value.Or(v2.Value).NotZero)
            {
                result = new LogicResult(0);
            }

            op.Result = result.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, result));
        }
    }
}