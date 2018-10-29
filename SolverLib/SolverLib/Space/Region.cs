using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;

namespace SolverLib.Space
{
    /// <summary>
    /// A set of keys that have a possible value. 
    /// Also references a set of constraints
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Region<TKey> : RegionBase, IRegion<TKey>
    {
        public Region()
        {
            Keys = new Keys<TKey>();
        }

        public Region(IEnumerable<TKey> keys, IPossible value) : base(value)
        {
            Keys = new Keys<TKey>(keys);
        }

        public Keys<TKey> Keys { get; set; }


    }
}
