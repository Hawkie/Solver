using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Puzzle;

namespace SolverLib.Engine
{
    public abstract class SolverBase<TKey>
    {

        /// <summary>
        /// Gets or sets the space and constraints
        /// </summary>
        public IPuzzle<TKey> Puzzle { get; set; }

        /// <summary>
        /// Gets or sets the Engine
        /// </summary>
        public IPuzzleEngine<TKey> Engine { get; set; }
    }
}
