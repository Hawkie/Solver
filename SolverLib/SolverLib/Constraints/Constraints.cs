using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Job;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using SolverLib.Space;

namespace SolverLib.Constraints
{
    /// <summary>
    /// This is a collection of constraints that can be referenced by key.
    /// There are 27 constraints in suduko. they can be referenced from each key
    /// </summary>
    public class Constraints<TKey> : HashSet<IConstraint<TKey>>, IConstraints<TKey>
    {

        /// <summary>
        /// This finds all constraints at the specified keys 
        /// It finds the keys in that constraint that intersect with the keys that have changed
        /// and creates a job for each 
        /// </summary>
        /// <param name="keysChangedIn">The set of keys that have just changed</param>
        /// <returns>The set of jobs created</returns>
        public int CreateSearchJobs(Keys<TKey> keysChanged, IPuzzleEngine<TKey> engine)
        {
            int added = 0;
            if (keysChanged.Count() > 0)
            {
                foreach (IConstraint<TKey> constraint in this)
                {
                   added += constraint.CreateSearchJobs(keysChanged, engine);
                   }
            }
            return added;
        }

        /// <summary>
        /// Find all constraints that contain all the keys
        /// E.g. Find me a constraint that contains the set 1,5,6
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="me"></param>
        /// <returns></returns>
        public IConstraints<TKey> FindOtherConstraintsContainingAllKeys(Keys<TKey> keys, ICollection<ConstraintType> types, IConstraints<TKey> excluding)
        {
            IConstraints<TKey> constraints = new Constraints<TKey>();
            // look through contraints that intesect at each of our combinations
            foreach (IConstraint<TKey> constraint in this)
            {
                if (types.Contains(constraint.Type))
                {
                    if (!excluding.Contains(constraint))
                    {
                        bool allPresent = true;
                        foreach (TKey key in keys)
                        {
                            if (!constraint.Keys.Contains(key))
                            {
                                allPresent = false;
                                break;
                            }
                              
                        }
                        if (allPresent)
                            constraints.Add(constraint);
                    }
                }
            }
            return constraints;
        }

    }
}
