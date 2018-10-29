using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;

namespace SolverLib.Space
{
    public interface IKeyedRegions<TKey> : IRegion<TKey>
    {
        IList<Keys<TKey>> OtherKeys { get; set; }
        

        
    }
}
