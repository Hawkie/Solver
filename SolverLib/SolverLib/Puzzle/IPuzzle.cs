using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Space;

namespace SolverLib.Puzzle
{
    public interface IPuzzle<TKey>
    {
        IConstraints<TKey> Constraints { get; set;}
        ISpace<TKey> Space { get; set; }



    }
}
