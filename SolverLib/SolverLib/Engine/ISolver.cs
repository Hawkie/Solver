using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Puzzle;

namespace SolverLib.Engine
{
    public interface ISolver<TKey>
    {
        /// <summary>
        /// Gets or sets the space and constraints
        /// </summary>
        IPuzzle<TKey> Puzzle { get; set; }

        /// <summary>
        /// Gets or sets the Engine
        /// </summary>
        IPuzzleEngine<TKey> Engine { get; set; }
    }
}
