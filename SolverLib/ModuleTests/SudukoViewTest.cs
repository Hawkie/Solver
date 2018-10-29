using System.Threading;
using System.Windows.Threading;
using SolverModules.Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Collections.Generic;

namespace ModuleTests
{
    
    
    /// <summary>
    ///This is a test class for SudukoViewTest and is intended
    ///to contain all SudukoViewTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SudukoViewTest
    {


        private TestContext testContextInstance;

        private bool solverFinished;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeShutdown();
        }
        
        #endregion


        /// <summary>
        ///A test for SudokuView Constructor
        ///</summary>
        [TestMethod()]
        public void SudukoViewConstructorTest()
        {
            Window fakeParentWindow = new Window();
            
            SudokuView target = new SudokuView();
            fakeParentWindow.Content = target;

            fakeParentWindow.Show();

            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(TestSteps1), null); 
            System.Windows.Threading.Dispatcher.Run();

            
        }

        private delegate object CallBack(object obj);

        public object TestSteps1(object obj)
        {
            
            System.Windows.Threading.Dispatcher.ExitAllFrames();
            return obj;
        }


        
        /// <summary>
        ///A test for an Eliminate Test
        ///</summary>
        [TestMethod()]
        public void SudukoSolveEliminateTest1()
        {
            SudokuSolver solver = new SudokuSolver();
            solver.Load("C:\\projects\\src\\2009\\Solver\\TestEliminate1.txt");
            int remaining = solver.Puzzle.Space.Unsolved();
            Assert.AreEqual(81 - 9, remaining, "Did not read 9 spaces");
            solver.Engine.Solve(false);
            remaining = solver.Puzzle.Space.Unsolved();
            Assert.AreEqual(81-12,remaining, "Did not solve 3 spaces");
            Assert.AreEqual(4, solver.Puzzle.Space[4].Resolve(), "Space 4 is not equal to 4");
            Assert.AreEqual(5, solver.Puzzle.Space[5].Resolve(), "Space 5 is not equal to 5");
            Assert.AreEqual(6, solver.Puzzle.Space[6].Resolve(), "Space 6 is not equal to 6");
        }

        /// <summary>
        ///A test for an Eliminate Test
        ///</summary>
        [TestMethod()]
        public void SudukoSolveEliminateTest2()
        {
            SudokuSolver solver = new SudokuSolver();
            solver.Load("C:\\projects\\src\\2009\\Solver\\TestEliminate2.txt");
            int remaining = solver.Puzzle.Space.Unsolved();
            Assert.AreEqual(81 - 8, remaining, "Read:Did not read 8 spaces");

            solverFinished = false;
            solver.Engine.SolverFinishedEvent += Engine_SolverFinishedEvent;
            solver.Engine.Solve(true);

            while (!solverFinished)
                Thread.Sleep(1000);

            remaining = solver.Puzzle.Space.Unsolved();
            Assert.AreEqual(81 - 9, remaining, "Solve:Did not solve 1 space");
            Assert.AreEqual(1, solver.Puzzle.Space[1].Resolve(), "Space 1 is not equal to 1");
        }

        void Engine_SolverFinishedEvent()
        {
            solverFinished = true;
        }

 

                /// <summary>
        ///A test for Dictionay
        ///</summary>
        [TestMethod()]
        public void DictionaryTest()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary[6] = "data6";
            string s = dictionary[6];
            Assert.AreEqual("data6",s);
        }

    }
}
