using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Space;

namespace SolverLib.Algorithms
{
    public class SetProcess<TKey> : ISetProcess<TKey>
    {
        private ICollection<IRegion<TKey>> regions = new HashSet<IRegion<TKey>>();

        public ICollection<IRegion<TKey>> Regions
        {
            get
            {
                return regions;
            }

            set
            {
                regions = value;
            }
        }

        public void Do(Keys<TKey> set, Possible values)
        {
            Regions.Add(new Region<TKey>(set, values));
        }
    }
}
