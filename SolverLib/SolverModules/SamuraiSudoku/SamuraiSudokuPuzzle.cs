using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.Puzzle;
using SolverLib.Space;
using SolverModules.Sudoku;

namespace SolverModules.SamauraiSudoku
{
    public class SamuraiSudokuPuzzle : SudokuPuzzleBase, IPuzzle<int>
    {
        public SamuraiSudokuPuzzle()
        {
            SetupSpace(21);
            SetupConstraints(21);
        }

        private void SetupSpace(int xWidth)
        {
            Space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            // Create 4x9x9 + 5x3x3 = 324+60 = 364 space with possible values of 1 to 9
            SetupSpaceGrid(0, 0, 9, 9, xWidth);
            SetupSpaceGrid(12, 0, 9, 9, xWidth);
            SetupSpaceGrid(9, 6, 3, 3, xWidth);
            SetupSpaceGrid(6, 9, 9, 3, xWidth);
            SetupSpaceGrid(9, 12, 3, 3, xWidth);
            SetupSpaceGrid(0, 12, 9, 9, xWidth);
            SetupSpaceGrid(12, 12, 9, 9, xWidth);
            
        }



        private void SetupConstraints(int xWidth)
        {
            SetupConstraints9x9(0, 0, 9, 9, xWidth, Constraints);
            SetupConstraints9x9(12, 0, 9, 9, xWidth, Constraints);
            SetupConstraints9x9(0, 12, 9, 9, xWidth, Constraints);
            SetupConstraints9x9(12, 12, 9, 9, xWidth, Constraints);
            // Row Exclusive Constraints
            AddRows(6, 6, 9, 9, xWidth, Constraints);

            // Column Exclusive Constraints
            AddColumns(6, 6, 9, 9, xWidth, Constraints);
            // Grid Exclusive Constraints
            AddGrid(9, 6, 3, 3, xWidth, Constraints);
            // Grid Exclusive Constraints
            AddGrid(6, 9, 3, 3, xWidth, Constraints);
            // Grid Exclusive Constraints
            AddGrid(9, 9, 3, 3, xWidth, Constraints);
            // Grid Exclusive Constraints
            AddGrid(12, 9, 3, 3, xWidth, Constraints);
            // Grid Exclusive Constraints
            AddGrid(9, 12, 3, 3, xWidth, Constraints);
        }

        private void SetupConstraints9x9(int xOffset, int yOffset, int xSize, int ySize, int xWidth, IConstraints<int> constraints)
        {
            // Row Exclusive Constraints
            AddRows(xOffset, yOffset, xSize, ySize, xWidth, Constraints);

            // Column Exclusive Constraints
            AddColumns(xOffset, yOffset, xSize, ySize, xWidth, Constraints);

            // Grid Exclusive Constraints
            AddGrids(xOffset, yOffset, xSize, ySize, xWidth, Constraints);
        }

    }
}
