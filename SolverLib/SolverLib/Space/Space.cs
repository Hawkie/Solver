using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using SolverLib.Core;

namespace SolverLib.Space
{
    // TODO load and save methods
    [Serializable]
    public class Space<TKey> : Dictionary<TKey, IPossible>, ISpace<TKey>, ISerializable
    {
        public Space(IPossible defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public Space(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }

        public object GetObject()
        {
            ArrayList a = new ArrayList();
            foreach (KeyValuePair<TKey, IPossible> pair in this)
            {
                object p = pair.Value.GetObject();
                a.Add(p);
            }
            return a;
        }

        public override string ToString()
        {
            string s = string.Empty;
            foreach (KeyValuePair<TKey, IPossible> pair in this)
            {
                s += "[" + pair.Key.ToString() + "," + pair.Value.ToString() + "]\n";
            }
            return s;
        }

        public void Parse(string s)
        {
            
        }

        public IPossible DefaultValue { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public void Reset()
        {
            foreach (KeyValuePair<TKey, IPossible> pair in this)
            {
                pair.Value.Locked = false;
                pair.Value.SetValues(new Possible(DefaultValue));
                pair.Value.AcceptChanges();
            }
        }

        public HashSet<TKey> GetAllKeys()
        {
            HashSet<TKey> keys = new HashSet<TKey>();
            foreach (TKey key in Keys)
            {
                keys.Add(key);
            }
            return keys;
        }

        public IPossible AllValuesAt(IEnumerable<TKey> keysInner)
        {
            Possible possibleInner = new Possible();
            foreach (TKey k in keysInner)
            {
                possibleInner.UnionPossible(this[k]);
            }
            return possibleInner;
        }

        public int Unsolved()
        {
            int remaining = 0;
            foreach (KeyValuePair<TKey, IPossible> location in this)
            {
                if (location.Value.Values.Count != 1)
                    remaining++;
            }
            return remaining;
        }

        public Keys<TKey> Eliminate(Keys<TKey> keys, IPossible valuesToRemove)
        {
            Keys<TKey> keysChanged = new Keys<TKey>();
            foreach (TKey k in keys)
            {
                // only add to changed if the keys that have changed
                if (this[k].FilterOut(valuesToRemove))
                    keysChanged.Add(k);
            }
            return keysChanged;
        }
    }
}
