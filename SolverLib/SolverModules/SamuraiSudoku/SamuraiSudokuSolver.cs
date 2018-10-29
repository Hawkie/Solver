using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Reader;
using SolverLib.Space;
using SolverModules.Sudoku;

namespace SolverModules.SamauraiSudoku
{
    public class SamuraiSudokuSolver : SolverBase<int>, ISolver<int>
    {
        public SamuraiSudokuSolver()
        {
            Puzzle = new SamuraiSudokuPuzzle();
            Engine = new PuzzleEngine<int>(Puzzle);
        }

        public void Load(string filename)
        {
            PuzzleReader reader = new PuzzleReader();
            IList<int> list = reader.Read(filename);
            ISpace<int> initialValues = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            reader.ConvertToInitialValues(list, initialValues);
            Engine.SetInitialValues(initialValues);
        }

    }
}
