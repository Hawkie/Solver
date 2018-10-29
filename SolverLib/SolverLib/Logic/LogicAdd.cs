using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicAdd : LogicNode
    {
        public LogicAdd()
        {
            
        }

        // Add more constructors later
        public LogicAdd(ILogicLeaf leafL, ILogicLeaf leafR)
        {
            this.Add(new LogicNodeLeaf(leafL));
            this.Add(new LogicNodeLeaf(leafR));
        }

        public override void Parse(object data, ILogicStack stack)
        {
            ILogicResult sum = new LogicResult(0);
            ILogicOperation op = new LogicOperation("Add", true);
            foreach (ILogicNode node in this)
            {
                node.Parse(data, stack);
                KeyValuePair<ILogicOperation, ILogicResult> item = stack.Pop();
                sum = sum.Add(item.Value);
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
