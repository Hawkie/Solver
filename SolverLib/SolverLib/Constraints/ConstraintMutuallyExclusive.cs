using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Job;
using SolverLib.Algorithms;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using SolverLib.Reader;
using SolverLib.SetTesters;
using SolverLib.Space;

namespace SolverLib.Constraints
{
    
    public class ConstraintMutuallyExclusive<TKey> : ConstraintBase<TKey>, IConstraint<TKey>
    {
        
        public ConstraintMutuallyExclusive(string name, Keys<TKey> group)
            : base(ConstraintType.MutuallyExclusive, name, group)
        {
            
        }

        /// <summary>
        /// find all the groups of keys (cGroups) in its own group (xGroup)
        /// that have a union value equal to the values (kValues) at kSource.
        /// </summary>
        /// <param name="keysChangedIn"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public int CreateSearchJobs(Keys<TKey> keysChanged, IPuzzleEngine<TKey> engine)
        {
            // Intersection = AND
            // Except = NOT
            // Union = OR
            int added = 0;
            IEnumerable<TKey> ourKeysChanged = Keys.Intersect(keysChanged);
            if (ourKeysChanged.Count() > 0)
            {
                // individual constraint actions. 
                // Must complete all 1 actions before attempting 2,3 actions
                added += CreateActions1(ourKeysChanged, engine);
                
                // Actions 2 and 3 are not change dependent. They work on this
                // constraint keys.
                //if (added == 0)
                //{
                    // Intersecting one set actions
                    added += CreateActions2(ourKeysChanged, engine);
                //}

                //if (added == 0)
                //{
                    // XWing set elimination. 
                    added += CreateActions3(ourKeysChanged, engine);
                //}
            }
            return added;
        }

        /// <summary>
        /// This method creates eliminate actions for each complete set
        /// it finds in our constraint.
        /// </summary>
        /// <param name="keysChangedIn">This is the last change</param>
        /// <param name="engine">The engine and the puzzle data</param>
        protected int CreateActions1(IEnumerable<TKey> keysChangedIn, IPuzzleEngine<TKey> engine)
        {
            int added = 0;
            foreach (TKey kSource in keysChangedIn)
            {
                if (added == 0)
                {
                    // Get the values at this point
                    IPossible kPossible = engine.Puzzle.Space[kSource];
                    // single value - no need to find combinations
                    if (kPossible.Count == 1)
                    {
                        Keys<TKey> useKeys = new Keys<TKey>() {kSource};
                        added += CreateCompleteSetActions(useKeys, Keys, engine);
                    }
                    // multi value - get all combinations in our set
                    else if ((kPossible.Count > 1))
                    {
                        Keys<TKey> combinationSet = new Keys<TKey>(Keys);
                        combinationSet.Remove(kSource);
                        int iUpper = combinationSet.Count;
                        int iLower = kPossible.Count - 1;

                        for (int search = iLower; search < iUpper && added == 0; search++)
                        {
                            CompleteSetTester<TKey> tester = new CompleteSetTester<TKey>(kSource, engine.Puzzle.Space);
                            CombinationValues<TKey> combi = new CombinationValues<TKey>(search, combinationSet);
                            combi.Test = tester.CompleteSet2;
                            combi.FindOne = true;
                            if (combi.CalcCombinations() > 0)
                            {
                                foreach (IList<TKey> combination in combi.Combinations)
                                {
                                    Keys<TKey> completeSet = new Keys<TKey>(combination);
                                    added += CreateCompleteSetActions(completeSet, Keys, engine);
                                }
                            }
                        }
                    }
                }
            }
            return added;
        }

        static public int CreateCompleteSetActions(Keys<TKey> group1, Keys<TKey> group2, IPuzzleEngine<TKey> engine)
        {
            int added = 0;
            // This works out possibleInner (go to space and union all values);
            IPossible possibleInner = engine.Puzzle.Space.AllValuesAt(group1);
            // If the values in the set are equal to the
            // available spaces in the solution then we consider this set
            // complete and can eliminate it from the rest of the group
            // (e.g. 1 value in 1 cell, or 8 values in 8 cells)
            if (possibleInner.Values.Count == group1.Count())
            {
                // This finds the working group of locations (excludes the changed set)
                Keys<TKey> workingGroup = new Keys<TKey>(group2);
                workingGroup.ExceptWith(group1);

                IJob<TKey> job = new JobFilter<TKey>("CompleteSet", workingGroup, possibleInner);
                engine.Add(job);
                added++;
                // Get all the possible values from key and filter them out of the group
                
            }
            return added;
        }

        /// <summary>
        /// Find the intersecting constraints
        /// </summary>
        /// <param name="visitor">This must be a ICollection of IActions</param>
        /// <param name="keysChangedIn"></param>
        /// <param name="space"></param>
        /// <param name="constraints"></param>
        protected int CreateActions2(IEnumerable<TKey> keysChangedIn, IPuzzleEngine<TKey> engine)
        {
            int added = 0;
            // find other intersecting constraints of the same type
            if (engine.Puzzle.Constraints != null)
            {
                foreach (IConstraint<TKey> constraint in engine.Puzzle.Constraints)
                {
                    if (constraint.Type == this.Type
                        && constraint != this)
                    {
                        Keys<TKey> intersection = new Keys<TKey>(Keys);
                        intersection.IntersectWith(constraint.Keys);
                        if (intersection.Count > 1)
                        {
                            added += CreateCompleteIntersectActions(Keys,constraint.Keys, engine);
                        }
                    }
                }
            }
            return added;
        }

        /// <summary>
        /// This elimination works out if there is an intersection between mutually exclusive sets
        /// that contains any values that are in the intersection and not in the outersection of one set.
        /// If this is so, (and both sets must contain that value) then the values can be eliminated from
        /// the other set also. Only need - in addition to above, used when intersecting points > 1.
        /// </summary>
        static public int CreateCompleteIntersectActions(Keys<TKey> group1, Keys<TKey> group2, IPuzzleEngine<TKey> engine)
        {
            int added = 0;
            Keys<TKey> keysIntersection = new Keys<TKey>(group1);
            keysIntersection.IntersectWith(group2);
            if (keysIntersection.Count > 1)
            {
                Keys<TKey> keysOuter1 = new Keys<TKey>(group1);
                keysOuter1.ExceptWith(keysIntersection);
                Keys<TKey> keysOuter2 = new Keys<TKey>(group2);
                keysOuter2.ExceptWith(keysIntersection);

                IPossible valuesIntersect = engine.Puzzle.Space.AllValuesAt(keysIntersection);
                IPossible valuesOuter1 = engine.Puzzle.Space.AllValuesAt(keysOuter1);
                IPossible valuesOuter2 = engine.Puzzle.Space.AllValuesAt(keysOuter2);


                Possible filter2 = new Possible(valuesIntersect);
                filter2.FilterOut(valuesOuter1);
                if (filter2.Count > 0)
                {
                    engine.Add(new JobFilter<TKey>("Intersection", keysOuter2, filter2));
                    added++;

                }
                // find each value that is present in the intersection (and not in outer1)
                // and eliminate it from the set2

                // find each value that is present in the intersection (and not in outer)
                // and eliminate it from the set1
                Possible filter1 = new Possible(valuesIntersect);
                filter1.FilterOut(valuesOuter2);
                if (filter1.Count > 0)
                {
                    engine.Add(new JobFilter<TKey>("Intersection", keysOuter1, filter1));
                    added++;
                }
            }
            return added;
        }

        /// <summary>
        /// This method is looking for a set of 2 locations that have values 
        /// that the outer set does not have (isolated). It then attempts to find all intersecting 
        /// sets of these values (crossing sets) and then finds all combinations of these 
        /// crossing sets to find an isolated set that contains them both.
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="keysChangedIn"></param>
        /// <param name="engine"></param>
        protected int CreateActions3(IEnumerable<TKey> keysChangedIn, IPuzzleEngine<TKey> engine)
        {
            int added = 0;
            // Each region is an isolated set 
            if (engine.Puzzle.Constraints != null)
            {
                ReducedSetTester<TKey> tester = new ReducedSetTester<TKey>(Keys, engine.Puzzle.Space);
                CombinationValues<TKey> combi = new CombinationValues<TKey>(2, Keys);
                combi.Test = tester.ReducedSet;
                combi.Store = false;
                combi.CalcCombinations();
                
                // There will only be one region
                foreach (IRegion<TKey> isolatedSet in tester.Regions)
                {
                    IList<IList<IKeyedRegions<TKey>>> xwings = CreateXWing(isolatedSet, engine);
                    added += CreateXWingProcesses(xwings, engine);
                }
            }
            return added;
        }

        protected IList<IList<IKeyedRegions<TKey>>> CreateXWing(IRegion<TKey> isolatedSet, IPuzzleEngine<TKey> engine)
        {
            List<IList<IKeyedRegions<TKey>>> xwings = new List<IList<IKeyedRegions<TKey>>>();
            // Check each of our isolated set values
            foreach (int value in isolatedSet.Value)
            {
                IList<IKeyedRegions<TKey>> xwing = new List<IKeyedRegions<TKey>>();
                xwing.Add(new KeyedRegions<TKey>());
                xwing.Add(new KeyedRegions<TKey>());
                xwing[0].Value = new Possible() { value };
                xwing[1].Value = new Possible() { value };
                int perpSetId = 0;

                foreach (TKey key in isolatedSet.Keys) // Two isolated keys only
                {

                    // Find other constraints passing through "key"
                    IConstraints<TKey> perpConstraints = engine.Puzzle.Constraints.FindOtherConstraintsContainingAllKeys(
                        new Keys<TKey>() { key },
                        new HashSet<ConstraintType>() { this.Type },
                        new Constraints<TKey>() { this });

                    foreach (IConstraint<TKey> perpConstraint in perpConstraints)
                    {
                        // Check each location in other set
                        Keys<TKey> otherKeys = new Keys<TKey>();
                        foreach (TKey constraintKey in perpConstraint.Keys)
                        {
                            if (!constraintKey.Equals(key))
                            {
                                // check if the spaces have the same values as our reducedset visitor combination
                                if (engine.Puzzle.Space[constraintKey].Contains(value))
                                {
                                    // TODO need two xwing groups. One for each constraint (linked contraint is different)
                                    otherKeys.Add(constraintKey);
                                }
                            }
                        }
                        if (otherKeys.Count() > 0)
                        {
                            xwing[perpSetId].OtherKeys.Add(otherKeys);
                            xwing[perpSetId].Keys.Add(key);
                        }
                    }
                    perpSetId++;
                }
                xwings.Add(xwing);
            }
            return xwings;

        }

        int CreateXWingProcesses(IList<IList<IKeyedRegions<TKey>>> xwings, IPuzzleEngine<TKey> engine)
        {
            int added = 0;
            foreach (IList<IKeyedRegions<TKey>> xwing in xwings)
            {
                foreach (Keys<TKey> keys1 in xwing[0].OtherKeys)
                {
                    foreach (TKey key1 in keys1)
                    {
                        foreach (Keys<TKey> keys2 in xwing[1].OtherKeys)
                        {
                            foreach (TKey key2 in keys2)
                            {
                                Keys<TKey> keysA = new Keys<TKey>() { key1, key2 };
                                IConstraints<TKey> y = engine.Puzzle.Constraints.FindOtherConstraintsContainingAllKeys(
                                    keysA,
                                    new HashSet<ConstraintType>() { this.Type },
                                    new Constraints<TKey>() { this });
                                foreach (IConstraint<TKey> constraint in y)
                                {
                                    if (xwing[0].Value.IsSubsetOf(keysA.ReducedSetValues(constraint.Keys, engine.Puzzle.Space)))
                                    {
                                        Keys<TKey> keys1R = new Keys<TKey>(keys1);
                                        keys1R.Remove(key1);
                                        if (keys1R.Count > 0)
                                        {
                                            //SpaceAdapter<int>.ToFile(engine.Puzzle.Space.ToString(), 0);
                                            JobFilter<TKey> action1 =
                                            new JobFilter<TKey>("XWing", keys1R, xwing[0].Value);
                                            engine.Add(action1);
                                            added++;                  
                                        }
                                        Keys<TKey> keys2R = new Keys<TKey>(keys2);
                                        keys2R.Remove(key2);
                                        if (keys2R.Count > 0)
                                        {
                                            //SpaceAdapter<int>.ToFile(engine.Puzzle.Space.ToString(), 2);
                                            JobFilter<TKey> action2 =
                                                new JobFilter<TKey>("XWing", keys2R, xwing[1].Value);
                                            engine.Add(action2);
                                            added++;
                                            //SpaceAdapter<int>.ToFile(engine.Puzzle.Space.ToString(), 3);
                                            
                                            
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return added;
        }
       
    }
}


