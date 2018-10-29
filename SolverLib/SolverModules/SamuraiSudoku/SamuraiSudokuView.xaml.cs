using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolverLib.Core;
using SolverModules.Sudoku;

namespace SolverModules.SamauraiSudoku
{
    /// <summary>
    /// Interaction logic for SamuraiSudokuView.xaml
    /// </summary>
    public partial class SamuraiSudokuView : UserControl
    {
        /// <summary>
        /// Gets or sets a solver object
        /// </summary>
        public SamuraiSudokuSolver Solver { get; private set; }

        public SamuraiSudokuView()
        {
            Solver = new SamuraiSudokuSolver();
            InitializeComponent();
            SetupControls(21);
            SetupCells(21);
        }

        private void SetupControls(int xWidth)
        {
            for (int i = 0; i < xWidth; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Name = "col" + i.ToString();
                col.Width = new GridLength(30);
                this.grid1.ColumnDefinitions.Add(col);

                RowDefinition row = new RowDefinition();
                row.Name = "row" + i.ToString();
                row.Height = new GridLength(30);
                this.grid1.RowDefinitions.Add(row);

            }
        }

        private void SetupCells(int xWidth)
        {
            foreach (KeyValuePair<int, IPossible> pair in Solver.Puzzle.Space)
            {
                UserControl l = CreateControl(pair.Key);
                int x = 0;
                int y = Math.DivRem(pair.Key-1, xWidth, out x);
                Grid.SetColumn(l, x);
                Grid.SetRow(l, y);
                grid1.Children.Add(l);
            }
        }

        public UserControl CreateControl(int key)
        {
            return new SudokuCell(Solver, key);
        }

        private void solveButton_Click(object sender, RoutedEventArgs e)
        {
            Solver.Engine.Solve(true);
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            Solver.Puzzle.Space.Reset();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Solver.Load(@"C:\src\Code\samuraiFiendish.txt");
        }
    }
}
