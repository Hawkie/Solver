using System;
using System.Collections.Generic;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Reader;
using SolverLib.Space;
using System.Linq;
using SolverModules.Sudoku;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for PuzzleBaseTest and is intended
    ///to contain all PuzzleBaseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PuzzleBaseTest
    {


        private TestContext testContextInstance;

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
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Solve
        ///</summary>
        public void SolveTestHelper<TKey>()
        {
            SudokuPuzzle sudokuPuzzle = new SudokuPuzzle();
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(sudokuPuzzle);

            // Read values
            PuzzleReader reader = new PuzzleReader();
            const string filename = "C:\\projects\\src\\2009\\Solver\\superfiend.txt";
            IList<int> list = reader.Read(filename);

            ISpace<int> initialValues = new Space<int>(new Possible() {1, 2, 3, 4, 5, 6, 7, 8, 9});
            reader.ConvertToInitialValues(list,initialValues);

            engine.SetInitialValues(initialValues);
            int remaining = sudokuPuzzle.Space.Unsolved();
            Assert.AreEqual(53, remaining, "Did not read 28 values");

            // debug handler
            foreach (KeyValuePair<int, IPossible> pair in engine.Puzzle.Space)
            {
                pair.Value.NoValuesLeft += new Possible.NoValuesLeftFunction(Value_NoValuesLeft);
            }

            //const bool expected = true; 
            engine.Solve(false);
            remaining = sudokuPuzzle.Space.Unsolved();
            SpaceAdapter<int>.WriteConsole(sudokuPuzzle.Space);
            
            string message = string.Format("Puzzle did not get solved. {0} spaces remaining.", remaining);
            Assert.AreEqual(0, engine.Count, "Not all tasks have completed");
            Assert.AreEqual(0, remaining, message);
        }

        void Value_NoValuesLeft(IPossible possible)
        {
            throw new Exception("No Values Left");
        }

        [TestMethod()]
        public void SolveTest()
        {
            SolveTestHelper<GenericParameterHelper>();
        }
    }
}
