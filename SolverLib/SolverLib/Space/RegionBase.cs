using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Space
{
    public abstract class RegionBase
    {
        public RegionBase()
        {
            Value = new Possible();
        }

        public RegionBase(IPossible value)
        {
            Value = new Possible(value);
        }

        public IPossible Value { get; set; }
    }
}
