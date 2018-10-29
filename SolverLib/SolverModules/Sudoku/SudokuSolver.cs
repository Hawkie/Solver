using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using SolverLib.Reader;
using SolverLib.Space;

namespace SolverModules.Sudoku
{
    public class SudokuSolver : SolverBase<int>, ISolver<int>
    {
        PuzzleReader PuzzleReader { get; set; }

        public SudokuSolver()
        {
            Puzzle = new SudokuPuzzle();
            Engine = new PuzzleEngine<int>(Puzzle);
            PuzzleReader = new PuzzleReader();
            Engine.SolverFinishedEvent += Engine_SolverFinishedEvent;
        }

        void Engine_SolverFinishedEvent()
        {
            
        }

        public void Load(string filename)
        {
            
            IList<int> list = PuzzleReader.Read(filename);
            ISpace<int> initialValues = new Space<int>(new Possible(){1,2,3,4,5,6,7,8,9});
            PuzzleReader.ConvertToInitialValues(list, initialValues);
            Engine.SetInitialValues(initialValues);
        }


        


    }

    
}
