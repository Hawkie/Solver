using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Core
{
    public class SetComparer<E,EK> : IEqualityComparer<E> where E : IEnumerable<EK>
    {
        public bool Equals(E x, E y)
        {
            //HashSet<EK> set1 = new HashSet<EK>(x);
            //HashSet<EK> set2 = new HashSet<EK>(y);
            //return set1.SetEquals(set2);
            return x.Equals(y);
        }

        // Sets are order independent
        public int GetHashCode(E x)
        {
            int code = 0;
            foreach (EK i in x)
            {
                code += i.GetHashCode();
            }
            return code;
        }
    }
}
