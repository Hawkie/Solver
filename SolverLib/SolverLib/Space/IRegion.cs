using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;

namespace SolverLib.Space
{
    /// <summary>
    /// A Region is an area (set of keys) that have a value associated with it.
    /// The value is simply for information processing (e.g. a test to perform)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRegion<TKey> : IRegionBase
    {
        Keys<TKey> Keys { get; set; }

        
    }
}
