using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Space
{
    /// <summary>
    /// This class can represent a set of possible values.
    /// E.g. Ac, kd, any, any, 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Combination<TKey> : HashSet<KeyValuePair<TKey, Possible>>, ICombination<TKey>
    {
    }
}
