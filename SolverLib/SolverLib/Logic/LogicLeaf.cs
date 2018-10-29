using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicLeaf : ILogicLeaf
    {
        private ILogicResult singleValue = new LogicResult(0);

        public LogicLeaf()
        {
            
        }

        public LogicLeaf(ILogicResult value)
        {
            this.singleValue = value;
            
        }

        public LogicLeaf(string operation, ILogicResult value)
        {
            this.singleValue = value;
            this.operation = operation;
        }

        private string operation = string.Empty;

        public string Operation
        {
            get
            {
                string leafName = string.Empty;
                if (this.GetType() != typeof(LogicLeaf))
                {
                    leafName = this.GetType().Name;
                }
                return leafName;
            }
            
            set
            {
                operation = value;
            }
        }

        public ILogicResult Value
        {
            get
            {
                return singleValue;
            }
            
            set
            {
                singleValue = value;
            }
        }
        
        /// <summary>
        /// This check returns 0 when a perfect match is found
        /// -1 for one point/degree below the check
        /// +1 for one point/ degree above the check
        /// max/min int when no match at all
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual KeyValuePair<ILogicOperation, ILogicResult> Check(object data)
        {
            ILogicOperation op = new LogicOperation(this.Operation);
            op.Result = this.Value.Value.ToString();
            return new KeyValuePair<ILogicOperation, ILogicResult>(op, this.Value);
        }

    }
}
