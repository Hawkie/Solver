using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.Puzzle;
using SolverLib.Reader;
using SolverLib.Space;

namespace SolverModules.Sudoku
{
    public class SudokuPuzzle : SudokuPuzzleBase, IPuzzle<int>
    {
        public SudokuPuzzle()
        {
            SetupSpace();
            SetupConstraints();
        }

        /// <summary>
        /// This creates the initial solution space
        /// </summary>
        private void SetupSpace()
        {
            Space = new Space<int>(new Possible() {1, 2, 3, 4, 5, 6, 7, 8, 9});
            SetupSpaceGrid(0, 0, 9, 9, 9);
            //// debug handler
            //foreach (KeyValuePair<int, Possible> pair in this.Space)
            //{
            //    pair.Leaf.NoValuesLeft += Value_NoValuesLeft;
            //}
            //this.Space[9].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SudokuPuzzle_PropertyChanged);
        }

        //void SudokuPuzzle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    Possible p = sender as Possible;
        //    string s = Space[9].ToString();
        //    int y = 0;
        //}

        void Value_NoValuesLeft(Possible possible)
        {
            throw new Exception("Solver has caused all values to be eliminated");
        }

        

        private void SetupConstraints()
        {
            // Row Exclusive Constraints
            AddRows(0, 0, 9, 9, 9, Constraints);

            // Column Exclusive Constraints
            AddColumns(0, 0, 9, 9, 9, Constraints);

            // Grid Exclusive Constraints
            AddGrids(0, 0, 9, 9, 9, Constraints);
        }
    }
}
