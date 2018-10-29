using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic.Node2
{
    public class LogicBitwiseAndTrue : LogicNode2
    {
        // Add more constructors later
        public LogicBitwiseAndTrue(ILogicLeaf leafL, ILogicLeaf leafR)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(leafR));
        }

        public LogicBitwiseAndTrue(ILogicNode nodeL, ILogicLeaf leafR)
        {
            this.Add(nodeL);
            this.Add(new LogicNodeLeaf(leafR));
        }

        public LogicBitwiseAndTrue(ILogicLeaf leafL, int limit)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(new LogicLeaf(new LogicResult(limit))));
        }

        public LogicBitwiseAndTrue(ILogicNode node, int limit)
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

            ILogicOperation op = new LogicOperation("BitwiseAndTrue");
            op.Add(v2.Key);
            op.Add(v1.Key);
            ILogicResult result = new LogicResult(LogicResult.MinValue);
            
            if (v1.Value.And(v2.Value).NotZero)
            {
                result = new LogicResult(0);
            }

            op.Result = result.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, result));
        }
    }
}