using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
//using SolverLib.Core.ValueGroup;

namespace SolverLib.Core
{
    public interface IPossible : ICollection<int>, INotifyPropertyChanged, ISerializable, IEquatable<IPossible>
    {
        bool SetEquals(IEnumerable<int> set);

        bool HasChanged { get; }

        void AcceptChanges();

        ICollection<int> Values { get; }

        bool Locked { get; set; }

        bool FilterOut(IPossible possible);

        bool UnionPossible(IPossible possible);

        bool IntersectPossible(IPossible possible);

        // Tests
        bool IsSubsetOf(IPossible possible);

        bool IsSupersetOf(IPossible possible);

//        UpperLower MinMaxPossible<K>(IPossibleProperties<K, int> setValues) where K : IEnumerable<int>;

        bool SetValue(int value);

        bool SetValues(IPossible possible);

        int Resolve();

        object GetObject();

        event Possible.NoValuesLeftFunction NoValuesLeft;
    }
}
