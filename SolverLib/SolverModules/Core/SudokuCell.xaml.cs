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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Space;

namespace SolverModules.Sudoku
{
    /// <summary>
    /// Interaction logic for SudokuCell.xaml
    /// </summary>
    public partial class SudokuCell : UserControl
    {
        private int key { get; set;}
        private ISolver<int> solver { get; set; }
        private Storyboard storyboard = null;
        private ColorAnimation colorAnimation1;
        private ColorAnimation colorAnimation2;
        private SolidColorBrush myBackgroundBrush = new SolidColorBrush();
        

        public SudokuCell(ISolver<int> solver, int key)
        {
            this.solver = solver;
            this.key = key;
            InitializeComponent();
            this.textBlock1.TextAlignment = TextAlignment.Center;
            this.CreateAnimation();
            this.SetTextBlock();
            this.AttachChangeEvent();
            
        }

        public void AttachChangeEvent()
        {
            solver.Puzzle.Space[key].PropertyChanged += possible_PropertyChanged;
        }

        public void DetachChangeEvent()
        {
            solver.Puzzle.Space[key].PropertyChanged -= possible_PropertyChanged;
        }

        void possible_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ChangeAccepted")
            {
                Possible possible = sender as Possible;
                SetTextBlock();
            }
        }

        private void CreateAnimation()
        {
            myBackgroundBrush.Color = Colors.White;
            this.textBlock1.Background = myBackgroundBrush;
            this.textBlock1.RegisterName("TextBlockBackgroundBrush", myBackgroundBrush);

            Duration dTime1 = new Duration(TimeSpan.FromMilliseconds(1000)); 
            colorAnimation1 = new ColorAnimation(Colors.Red, Colors.Green, dTime1);
            Storyboard.SetTargetName(colorAnimation1, "TextBlockBackgroundBrush");
            Storyboard.SetTargetProperty(colorAnimation1, new PropertyPath("Color"));
            
            Duration dTime2 = new Duration(TimeSpan.FromMilliseconds(2000)); 
            colorAnimation2 = new ColorAnimation(Colors.Red, Colors.White, dTime2);
            Storyboard.SetTargetName(colorAnimation2, "TextBlockBackgroundBrush");
            Storyboard.SetTargetProperty(colorAnimation2, new PropertyPath("Color"));

            // you can't have a storyboard with two animations in it that target the same property!
            // For this use KeyFrames
            storyboard = new Storyboard();
            //storyboard.Children.Add(colorAnimation1);
            storyboard.Children.Add(colorAnimation2);

        }

        private void SetTextBlock()
        {
            //this.textBlock1.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation2);

            if (solver.Puzzle.Space[key].Locked)
            {
                SolidColorBrush yellowBrush = new SolidColorBrush(Colors.Yellow);
                this.textBlock1.Background = yellowBrush;
            }
            else
            {
                this.textBlock1.Background = myBackgroundBrush;

            }
                
            this.textBlock1.Text = PossibleText();

            double factor = 1;
            if (solver.Puzzle.Space[key].Values.Count > 0)
                factor = (1 / solver.Puzzle.Space[key].Values.Count) * 0.5;

            if (this.textBlock1.Text.Length > 1)
            {
                this.textBlock1.FontSize = 7;
                this.textBlock1.Opacity = 0.5 + factor;
                storyboard.Begin(textBlock1);
                
            }
            else
            {
                this.textBlock1.FontSize = 16;
                this.textBlock1.Opacity = 1;
                if (!solver.Puzzle.Space[key].Locked)
                    this.textBlock1.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation1);
            }
        }

        private string PossibleText()
        {
            string cellText = "";
            bool bStep1 = false;
            bool bStep2 = false;
            if (solver.Puzzle.Space[key].Count == 1)
                cellText = solver.Puzzle.Space[key].Resolve().ToString();
            else
            {
                for (int i=1;i< 10;i++)
                {
                    if (!bStep1 && i > 3 && i < 7)
                    {
                        cellText += "\n";
                        bStep1 = true;
                    }
                    if (!bStep2 && i > 6)
                    {
                        cellText += "\n";
                        bStep2 = true;
                    }
                    if (solver.Puzzle.Space[key].Contains(i))
                        cellText += i.ToString() + " ";
                    else
                    {
                        cellText += "  ";
                    }
                }
            }
            return cellText;
        }

        private void textBlock1_LostFocus(object sender, RoutedEventArgs e)
        {
            IPossible possibleOriginal = solver.Puzzle.Space[key];
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            IPossible p = new Possible();
            foreach (char c in textBlock1.Text)
            {
                if (c.CompareTo('1') == 0)
                    p.Add(1);
                else if (c.CompareTo('2') == 0)
                    p.Add(2);
                else if (c.CompareTo('3') == 0)
                    p.Add(3);
                else if (c.CompareTo('4') == 0)
                    p.Add(4);
                else if (c.CompareTo('5') == 0)
                    p.Add(5);
                else if (c.CompareTo('6') == 0)
                    p.Add(6);
                else if (c.CompareTo('7') == 0)
                    p.Add(7);
                else if (c.CompareTo('8') == 0)
                    p.Add(8);
                else if (c.CompareTo('9') == 0)
                    p.Add(9);
            }
            space.Add(key, p);
            solver.Engine.SetInitialValues(space);
        }

        private void textBlock1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textBlock1.SelectAll();
        }

        


        
        
    }
}
