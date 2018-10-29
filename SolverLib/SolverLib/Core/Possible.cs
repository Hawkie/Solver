using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
//using SolverLib.Core.ValueGroup;

namespace SolverLib.Core
{
    public class Possible : IPossible
    {
        private bool hasChanged = false;
        private HashSet<int> innerValue = new HashSet<int>();
        private bool locked = false;
        public bool Locked
        {
            get
            {
                return locked;    
            }
            set
            {
                locked = value;
            }
        }

        public Possible()
        {
        }

        public Possible(ICollection<int> collection)
        {
            innerValue = new HashSet<int>(collection);
        }

        public bool Equals(IPossible other)
        {
            return this.SetEquals(other);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return this.SetEquals(obj as IPossible);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int code = 0;
            foreach (int i in this)
            {
                code += i.GetHashCode();
            }
            return code;
        }

        public override string ToString()
        {
            string s = "[";
            bool first = true;
            foreach (int v in Values)
            {
                if (!first)
                {
                    s += ",";
                }
                else
                {
                    first = false;
                }
                s += v.ToString();
            }
            s += "]";
            return s;
        }
        
        public bool SetEquals(IEnumerable<int> possible)
        {
            return innerValue.SetEquals(possible);
        }

        public void Add(int value)
        {
            bool added = innerValue.Add(value);
            if (added)
            {
                HasChanged = true;
            }
        }

        public bool HasChanged
        {
            get
            {
                return hasChanged;
            }
            private set
            {
                // Always send the changed event. Useful if changing multiple times.
                hasChanged = value;
                OnPropertyChanged("HasChanged");

            }
        }

        public bool ChangeAccepted
        {
            get
            {
                return !hasChanged;
            }
        }

        /// <summary>
        /// Call this to fire the ChangeAccepted Property.
        /// </summary>
        public void AcceptChanges()
        {
            HasChanged = false;
            OnPropertyChanged("ChangeAccepted");
        }

        // This is a very specific display for Suduko!
        public ICollection<int> Values
        {
            get
            {
                // return a copy so they cannot affect our value
                return innerValue;
            }
            set
            {
                if (!innerValue.SetEquals(value))
                {
                    innerValue.Clear();
                    foreach (int i in value)
                    {
                        innerValue.Add(i);
                    }
                    HasChanged = true;
                }
            }
        }

        // Returns the actual values filtered 
        // e.g. 1-9 filter 1-3 (result = 4-9) return 1-3
        // e.g. 2-9 filter 1-3 (result = 4-9) return 2-3
        public bool FilterOut(IPossible possible)
        {
            int oldCount = innerValue.Count;
            innerValue.ExceptWith(possible.Values);
            if (innerValue.Count != oldCount)
            {
                HasChanged = true;
                if (innerValue.Count == 0)
                {
                    if (NoValuesLeft != null)
                    {
                        NoValuesLeft(this);
                    }
                }
                return true;
            }
            return false;
        }

        public bool UnionPossible(IPossible possible)
        {
            int oldCount = innerValue.Count;
            innerValue.UnionWith(possible.Values);
            if (innerValue.Count != oldCount)
            {
                HasChanged = true;
                return true;
            }
            return false;
        }

        // Test

        public bool IntersectPossible(IPossible possible)
        {
            int oldCount = innerValue.Count;
            innerValue.IntersectWith(possible.Values);
            if (innerValue.Count != oldCount)
            {
                HasChanged = true;
                return true;
            }
            return false;
        }

        public bool IsSubsetOf(IPossible possible)
        {
            return innerValue.IsSubsetOf(possible);
        }

        public bool IsSupersetOf(IPossible possible)
        {
            return innerValue.IsSupersetOf(possible);
        }

        /// <summary>
        /// This sets the value to the specified value
        /// </summary>
        /// <param name="value">Single possible value</param>
        /// <returns>The values that were removed</returns>
        public bool SetValue(int value)
        {
            if (Resolve() != value)
            {
                innerValue.Clear();
                innerValue.Add(value);
                HasChanged = true;
                return true;
            }
            return false;
        }

        public bool SetValues(IPossible possible)
        {
            if (!this.innerValue.SetEquals(possible))
            {
                this.innerValue = new HashSet<int>(possible.Values);
                HasChanged = true;
                return true;
            }
            return false;
        }

        public int Resolve()
        {
            if (innerValue.Count == 1)
                return innerValue.First();
            return 0;
        }

        #region PropertyChanged

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NoValuesLeftFunction(IPossible possible);

        public event NoValuesLeftFunction NoValuesLeft;

        #endregion

        #region IEnumerable<int> Members

        public IEnumerator<int> GetEnumerator()
        {
            return innerValue.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return innerValue.GetEnumerator();
        }

        #endregion

        #region ICollection<int> Members


        public void Clear()
        {
            if (innerValue.Count > 0)
            {
                innerValue.Clear();
                HasChanged = true;
            }
        }

        public bool Contains(int item)
        {
            return innerValue.Contains(item);
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return Values.Count;
            }

        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public bool Remove(int item)
        {
            bool bRemoved = innerValue.Remove(item);
            if (bRemoved)
            {
                HasChanged = true;
                return true;
            }
            return false;
        }

        #endregion

        #region Serialization

        public Possible(SerializationInfo info, StreamingContext context)
        {
            int size = info.GetInt32("size");
            for (int i = 0; i < size; i++)
            {
                innerValue.Add(info.GetInt32(i.ToString()));
            }
        }


        public object GetObject()
        {
            ArrayList a = new ArrayList();
            foreach (int i in innerValue)
            {
                a.Add(i);
            }
            return a;
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data. </param><param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization. </param><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("size", innerValue.Count);
            int index = 0;
            foreach (int i in innerValue)
            {
                info.AddValue(index.ToString(), i);
                index++;
            }
        }
        #endregion
    }

    
}
