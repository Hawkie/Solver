using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Space
{
    public interface ISpace<TKey> : IDictionary<TKey, IPossible>, ISerializable
    {
        object GetObject();

        void Reset();

        IPossible AllValuesAt(IEnumerable<TKey> keysInner);

        int Unsolved();

        HashSet<TKey> GetAllKeys();

        IPossible DefaultValue { get; }

        Keys<TKey> Eliminate(Keys<TKey> keys, IPossible valuesToRemove);
        
    }
}
