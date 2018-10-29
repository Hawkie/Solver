using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.NewAction;

namespace SolverLib.Actions
{
    public class ActionSetBase<TKey> : JobBase<TKey>
    {
        protected ActionSetBase(string name, JobType type, int priority)
            : base(name, type, priority)
        {

        }

        public Keys<TKey> Group1 { get; set; }

        public Keys<TKey> Group2 { get; set; }
    }
}
