using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Job
{
    public abstract class JobBase<TKey> 
    {
        public JobBase(string name, JobType type, int priority)
        {
            this.Name = name;
            this.Type = type;
            this.Priority = priority;
        }

        public string Name { get; protected set; }

        public JobType Type { get; protected set; }

        public int Priority { get; set; }

        

    }

}
