using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public interface ILogicResult : IComparable<ILogicResult>
    {
        object Value { get; set; }

        int Type { get; }

        int ToInt32 { get; set; }

        double PassRating { get; }

        ILogicResult Subtract(ILogicResult other);

        ILogicResult Add(ILogicResult other);

        ILogicResult Or(ILogicResult other);

        ILogicResult And(ILogicResult other);

        ILogicResult Combine(ILogicResult other);

        bool NotZero { get;}
    }
}
