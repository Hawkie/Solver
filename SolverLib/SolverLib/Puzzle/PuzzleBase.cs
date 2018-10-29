using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.Reader;
using SolverLib.Space;

namespace SolverLib.Puzzle
{
    public class PuzzleBase<TKey>  : IPuzzle<TKey>
    {
        public PuzzleBase()
        {
            // Initialise Space
            this.Constraints = new Constraints<TKey>();
        }

        public PuzzleBase(ISpace<TKey> space)
        {
            // Initialise Space
            this.Space = space;
            this.Constraints = new Constraints<TKey>();
        }



        public IConstraints<TKey> Constraints { get; set; }
        public ISpace<TKey> Space { get; set; }




    }
}
