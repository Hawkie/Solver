using System;
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
    public interface IConstraints<TKey> : ICollection<IConstraint<TKey> >
    {
        /// <summary>
        /// This finds all constraints at the specified keys 
        /// It finds the keys in that constraint that intersect with the keys that have changed
        /// and creates a job for each 
        /// </summary>
        /// <param name="keysChanged">The set of keys that have just changed</param>
        /// <returns>The set of jobs created</returns>
        int CreateSearchJobs(Keys<TKey> changed, IPuzzleEngine<TKey> engine);

        /// <summary>
        /// Find all constraints that contain all the keys
        /// E.g. Find me a constraint that contains the set 1,5,6
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="me"></param>
        /// <returns></returns>
        IConstraints<TKey> FindOtherConstraintsContainingAllKeys(Keys<TKey> keys, ICollection<ConstraintType> types,
                                                                 IConstraints<TKey> excluding);
        
    }
}
