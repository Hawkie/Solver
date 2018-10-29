using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicAnd : LogicNode
    {
        public LogicAnd()
        {
            
        }

        // Add more constructors later
        public LogicAnd(ILogicLeaf leafL, ILogicLeaf leafR)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(leafR));
        }

        public override void Parse(object data, ILogicStack stack)
        {
            ILogicResult sum = new LogicResult(0);
            ILogicOperation op = new LogicOperation("And", true);
            foreach (ILogicNode node in this)
            {
                node.Parse(data, stack);
                KeyValuePair<ILogicOperation, ILogicResult> item = stack.Pop();
                sum = sum.Combine(item.Value);
                //sum.PassRating += item.Value.PassRating;
                if (item.Key != null)
                {
                    op.Add(item.Key);
                }
            }

            op.Result = sum.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, sum));
        }
    }
}
