using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;

namespace SolverLib.Constraints
{
    public interface IVisitorChange<TKey>
    {
        void OnChange(Keys<TKey> keysChanged);
    }
}
