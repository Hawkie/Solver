using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;


namespace SolverLib.Core.ValueGroup
{
    public class SetOfSets<K> : HashSet<K>, ISetOfSets<K> where K : ICollection<int>
    {
        

        public SetOfSets()
            : base(new SetComparer<K,int>())
        { }
        
        public SetOfSets(IEqualityComparer<K> comparer) : base(comparer)
        {}

        //public IPossibleProperties<K, int> CategoryCount(ICollection<K> qualifiers)
        //{
        //    IPossibleProperties<K, int> properties = new PossibleProperties<K, int>();
        //    foreach (K qualify in qualifiers)
        //    {
        //        int count = 0;
        //        foreach (K item in this)
        //        {
        //            if (item.Intersect(qualify).Count() > 0)
        //            {
        //                // Increase count
        //                count++;
        //            }
        //        }
        //        properties.Add(qualify, count);
        //    }
        //    return properties;
        //}
        
        //public MinMaxValue MinMaxSum(IPossibleProperties<K, int> qualifyAndPoints)
        //{
        //    MinMaxValue minMaxSum = new MinMaxValue();
        //    foreach (K item in this)
        //    {
        //        IPossibleProperties<K, int> categorised = qualifyAndPoints.Categorise(item);
        //        minMaxSum.Min += categorised.Values.Min();
        //        minMaxSum.Max += categorised.Values.Max();
        //    }
        //    return minMaxSum;
        //}

        ///// <summary>
        ///// Item is the variable in each set.
        ///// </summary>
        ///// <param name="qualifyAndPoints"></param>
        ///// <returns></returns>
        //public SortedList<K, IList<int>> Categorised(IPossibleProperties<K, int> qualifyAndPoints) // TODO pass in FUnc
        //{
        //    SortedList<K, IList<int>> categorised = new SortedList<K, IList<int>>();
        //    foreach (K item in this)
        //    {
        //        IList<int> list = new List<int>();
                
        //        IPossibleProperties<K, int> category = qualifyAndPoints.Categorise(item);
        //        foreach (KeyValuePair<K, int> pair in category)
        //        {
        //            list.Add(pair.Value);
        //        }
        //        // functor
        //        categorised.Add(item, list);
        //    }
        //    return categorised;
        //}

        //public IPossibleProperties<K, int> Categorised(IPossibleProperties<K, int> qualifyAndPoints, Func<ICollection<int>, int> func)
        //{
        //    IPossibleProperties<K, int> list = new PossibleProperties<K, int>();
        //    foreach (K item in this)
        //    {
        //        IPossibleProperties<K, int> categorised = qualifyAndPoints.Categorise(item);
        //        list.Add(item, func(categorised.Values));
        //    }
        //    return list;
        //}

        //public int Categorised(IPossibleProperties<K, int> qualifyAndPoints, Func<ICollection<int>, int> func,
        //    Func<ICollection<int>, int> func2)
        //{
        //    IList<int> list = new List<int>();
        //    foreach (K item in this)
        //    {
        //        IPossibleProperties<K, int> categorised = qualifyAndPoints.Categorise(item);
        //        list.Add(func(categorised.Values));
        //    }
        //    int i = func2(list);
        //    return i;
        //}

        //public IPossibleProperties<K, int> MinCategorised(IPossibleProperties<K, int> qualifyAndPoints)
        //{
        //    IPossibleProperties<K, int> listMin = new PossibleProperties<K, int>();
        //    foreach (K item in this)
        //    {
        //        IPossibleProperties<K, int> categorised = qualifyAndPoints.Categorise(item);
        //        listMin.Add(item, categorised.Values.Min());
        //    }
        //    return listMin;
        //}

        //public IPossibleProperties<K, int> MaxCategorised(IPossibleProperties<K, int> qualifyAndPoints)
        //{
        //    IPossibleProperties<K, int> listMax = new PossibleProperties<K, int>();
        //    foreach (K item in this)
        //    {
        //        IPossibleProperties<K, int> categorised = qualifyAndPoints.Categorise(item);
        //        listMax.Add(item, categorised.Values.Max());
        //    }
        //    return listMax;
        //}


        //public bool DoesNotContainAny(ISetOfSets<K> group)
        //{
        //    return (this.Intersect(group).Count() == 0);
        //}


        //public bool ContainsAll(ISetOfSets<K> group)
        //{
        //    return this.IsSupersetOf(group);
        //}



    }
}
