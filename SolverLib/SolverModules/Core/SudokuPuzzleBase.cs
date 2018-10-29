using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.Puzzle;

namespace SolverModules.Sudoku
{
    public abstract class SudokuPuzzleBase : PuzzleBase<int>
    {

        public void SetupSpaceGrid(int xOffset, int yOffset, int xSize, int ySize, int xWidth)
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    Space.Add(1 + x + xOffset + ((y + yOffset)*xWidth), new Possible() {1, 2, 3, 4, 5, 6, 7, 8, 9});
                }
            }

        }

        public void AddRows(int xOffset, int yOffset, int xSize, int ySize, int xWidth, IConstraints<int> constraints)
        {
            // Row Exclusive Constraints
            for (int y = 0; y < ySize; y++)
            {
                Keys<int> group = new Keys<int>();
                for (int x = 0; x < xSize; x++)
                {
                    group.Add(1 + x + xOffset + ((y + yOffset) * xWidth));
                }
                string name = string.Format("Row {0}: At {1}", y + 1, xOffset + 1+ (yOffset * xWidth));
                IConstraint<int> constraint = new ConstraintMutuallyExclusive<int>(name, group);
                constraints.Add(constraint);
            }
        }

        public void AddColumns(int xOffset, int yOffset, int xSize, int ySize, int xWidth, IConstraints<int> constraints)
        {
            for (int x = 0; x < xSize; x++)
            {
                Keys<int> group = new Keys<int>();
                for (int y = 0; y < ySize; y++)
                {
                    group.Add(1 + x + xOffset + ((y + yOffset) * xWidth));
                }
                string name = string.Format("Column {0}: At {1}", x + 1, x + 1 + xOffset + (yOffset * xWidth));
                IConstraint<int> constraint = new ConstraintMutuallyExclusive<int>(name, group);
                constraints.Add(constraint);
            }
        }

        public void AddGrids(int xOffset, int yOffset, int xSize, int ySize, int xWidth, IConstraints<int> constraints)
        {
            for (int y2 = 0; y2 < ySize; y2 = y2 + 3)
            {
                for (int x2 = 0; x2 < xSize; x2 = x2 + 3)
                {
                    AddGrid(x2+xOffset,y2+yOffset,3,3,xWidth, constraints);
                }
            }
        }

        public void AddGrid(int xOffset, int yOffset, int xSize, int ySize, int xWidth, IConstraints<int> constraints)
        {
            Keys<int> group = new Keys<int>();
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    group.Add(1 + x + xOffset + ((y + yOffset) * xWidth));
                }
            }
            int xGrid = xOffset > 0 ? (xOffset/xSize) + 1 : 1;
            int yGrid = yOffset > 0 ? (yOffset*xWidth)/ySize + 1 : 1;
            string name = string.Format("Grid {0},{1}: At {2}", xGrid, yGrid , xOffset + 1 + (yOffset * xWidth));
            IConstraint<int> constraint = new ConstraintMutuallyExclusive<int>(name, group);
            constraints.Add(constraint);
        }
    }
}
