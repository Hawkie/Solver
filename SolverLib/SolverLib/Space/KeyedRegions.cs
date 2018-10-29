using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;

namespace SolverLib.Space
{
    public class KeyedRegions<TKey> : Region<TKey>, IKeyedRegions<TKey>
    {
        public KeyedRegions() 
        {
            OtherKeys = new List<Keys<TKey>>();
            
        }

        public IList<Keys<TKey>> OtherKeys { get; set; }

    }
}
