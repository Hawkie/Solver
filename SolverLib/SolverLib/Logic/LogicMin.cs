using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicMin : LogicNode
    {

        public override void Parse(object data, ILogicStack stack)
        {
            ILogicOperation op = new LogicOperation("Min");
            ILogicResult r1 = new LogicResult(LogicResult.MaxValue);
            foreach (ILogicNode node in this)
            {
                node.Parse(data, stack);
                KeyValuePair<ILogicOperation, ILogicResult> r = stack.Pop();
                if (r.Value.CompareTo(r1) < 0)
                {
                    // Set the minimum
                    r1.Value = r.Value.Value;
                }
                op.Add(r.Key);
            }
            op.Result = r1.Value.ToString();
            stack.Push(new KeyValuePair<ILogicOperation, ILogicResult>(op, r1));
        }
    }
}
