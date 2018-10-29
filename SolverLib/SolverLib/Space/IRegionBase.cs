using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Space
{
    public interface IRegionBase
    {
        IPossible Value { get; set; }
    }
}
