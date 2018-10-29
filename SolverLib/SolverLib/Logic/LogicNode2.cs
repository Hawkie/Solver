using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public abstract class LogicNode2 : LogicNode
    {
       /// <summary>
        /// The min, max, add, sub nodes all expect two nodes that have one result on its leaf
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override void Parse(object data, ILogicStack stack)
        {
            base.Parse(data, stack);
            if (stack.Count() < 2)
            {
                throw new InvalidExpressionException("Node should only have two sub nodes");
            }
        }
    }
}
