using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Job;
using SolverLib.Algorithms;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using SolverLib.Space;

namespace SolverLib.Constraints
{
    /// <summary>
    /// A constraint is the combination of a group of keys and a set of algorithms that
    /// it obeys. E.g. This group contains values that are mutually exclusive
    /// Suduko - a row that has 9 keys that are mutually exclusive
    /// </summary>
    public interface IConstraint<TKey> 
    {
        string Name { get; set; }
        ConstraintType Type { get; }

        Keys<TKey> Keys { get; set; }

        int CreateSearchJobs(Keys<TKey> keysChangedIn, IPuzzleEngine<TKey> engine);

        

        
        

        
    }
}
