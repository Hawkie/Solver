using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Space;

namespace SolverLib.Algorithms
{
    public interface ISetProcess<TKey>
    {
        ICollection<IRegion<TKey>> Regions { get; set; }

        void Do(Keys<TKey> set, Possible values);
        
    }
}
