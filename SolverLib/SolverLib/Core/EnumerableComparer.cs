using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Core
{
    public class EnumerableComparer<E, EK> : IComparer<E>, IEqualityComparer<E> where E : IEnumerable<EK> where EK : IComparable<EK>
    {
        public int Compare(E x, E y)
        {
            if (x.Count() > y.Count())
            {
                return 1;
            }
            if (x.Count() < y.Count())
            {
                return -1;
            }

            IEnumerator<EK> it1 = x.GetEnumerator();
            IEnumerator<EK> it2 = y.GetEnumerator();
            while (it1.MoveNext() && it2.MoveNext())
            {
                if (!it1.Current.Equals(it2.Current))
                {
                    return it1.Current.CompareTo(it2.Current);
                }
            }
            return 0;
        }

        public bool Equals(E x, E y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(E x)
        {
            int code = 0;
            int index = 0;
            foreach (EK i in x)
            {
                code += i.GetHashCode() * index;
            }
            return code;
        }

        

    }
}
