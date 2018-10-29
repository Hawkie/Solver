using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Algorithms;
using SolverLib.Core;
using SolverLib.Space;

namespace SolverLib.Constraints
{
    public abstract class ConstraintBase<TKey>
    {
        public ConstraintBase(ConstraintType type, string name, Keys<TKey> keys)
        {
            this.type = type;
            this.Name = name;
            this.keys = keys;
        }

        private Keys<TKey> keys = new Keys<TKey>();

        protected ConstraintType type = ConstraintType.Invalid;
        
        public string Name { get; set; }

        public ConstraintType Type
        {
            get
            {
                return type;
            }
        }

        public Keys<TKey> Keys
        {
            get
            {
                return keys;
            }
            set
            {
                keys = value;
            }
        }

        protected IEnumerable<TKey> FindIntersectingKeys(Keys<TKey> keysChangedIn)
        {
            return Keys.Intersect(keysChangedIn);
        }
    }
}

