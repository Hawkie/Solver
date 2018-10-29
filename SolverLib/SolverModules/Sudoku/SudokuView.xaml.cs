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
using SolverLib.Space;

namespace SolverModules.Sudoku
{
    /// <summary>
    /// Interaction logic for SudokuView.xaml
    /// </summary>
    public partial class SudokuView : UserControl
    {
        
        public SudokuView()
        {
            Solver = new SudokuSolver();
            InitializeComponent();
            grid1.ShowGridLines = false;
            for (int i = 0; i < 9; i++ )
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
            SetupCells(9);
  
            this.DataContext = this.Solver;
        }

        private void SetupCells(int xWidth)
        {
            foreach (KeyValuePair<int, IPossible> pair in Solver.Puzzle.Space)
            {
                UserControl l = CreateControl(pair.Key);
                int x = 0;
                int y = Math.DivRem(pair.Key - 1, xWidth, out x);
                Grid.SetColumn(l, x);
                Grid.SetRow(l, y);
                grid1.Children.Add(l);
            }
        }

        public UserControl CreateControl(int key)
        {
            return new SudokuCell(Solver, key);
        }

        /// <summary>
        /// Gets or sets a solver object
        /// </summary>
        public SudokuSolver Solver { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Solver.Engine.Solve(true);
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Solver.Load(@"..\..\puzzles\puzzlefiend.txt");
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            Solver.Puzzle.Space.Reset();
        }
        
    }
}
