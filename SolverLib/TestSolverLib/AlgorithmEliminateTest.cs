using System.Collections.Generic;
using System.Linq;
using SolverLib;
using SolverLib.Job;
using SolverLib.Core;
using SolverLib.Constraints;
using SolverLib.Algorithms;
using SolverLib.Engine;
using SolverLib.Puzzle;
using SolverLib.Space;

using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for AlgorithmEliminateTest and is intended
    ///to contain all AlgorithmEliminateTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AlgorithmEliminateTest
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
        ///A test for RunAlgorithm
        ///</summary>
        [TestMethod()]
        public void RunAlgorithmTest()
        {
            // Initialise Space
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }); 
            for (int i = 1; i < 82; i++)
            {
                space.Add(i, new Possible(){1,2,3,4,5,6,7,8,9});
            }

            // Initialise one group (top row)
            Keys<int> group = new Keys<int>(){1,2,3,4,5,6,7,8,9};
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            space[1].SetValue(1);
            Keys<int> keysInner = new Keys<int>(){1};
            int jobsAdded = ConstraintMutuallyExclusive<int>.CreateCompleteSetActions(keysInner, group, engine);
            

            // Test
            Keys<int> expectedK = new Keys<int>(){2,3,4,5,6,7,8,9};
            Possible expectedV = new Possible(){1};
            IJobFilter<int> job = engine.Peek() as IJobFilter<int>;
            Keys<int> actual = job.Keys;
            IPossible filter = job.Filter;
            // Check the algorithm returned 8 changes
            Assert.AreEqual(8, actual.Count(), "Algorithm Eliminate did not return 8 changes");
            Assert.IsTrue(expectedK.SetEquals(actual));
            Assert.AreEqual(1, filter.Count,"Filter value incorrect");
            Assert.IsTrue(expectedV.SetEquals(filter), "Filter values incorrect");            
        }

        [TestMethod()]
        public void RunAlgorithmEliminateTest1()
        {
            // Initialise Possible
            IPossible possibleAll = new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
            // Initialise Space
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 82; i++)
            {
                space.Add(i, new Possible(possibleAll));
            }

            // Initialise one group (top row)
            Keys<int> group = new Keys<int>();
            for (int i = 1; i < 10; i++)
            {
                group.Add(i);
            }
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            Possible values2To9 = new Possible(possibleAll);
            values2To9.Remove(1);
            Keys<int> keysInner = new Keys<int>();
            for (int j = 2; j < 10; j++)
            {
                keysInner.Add(j);
                space[j] = new Possible(values2To9);
            }
            int jobsAdded = ConstraintMutuallyExclusive<int>.CreateCompleteSetActions(keysInner, group, engine);
            Assert.AreEqual(1, engine.Count);

            // Test
            Keys<int> expectedK = new Keys<int>(){1};
            IPossible expectedV = new Possible(){2,3,4,5,6,7,8,9};

            IJobFilter<int> job = engine.Peek() as IJobFilter<int>;
            Keys<int> actual = job.Keys;
            IPossible filter = job.Filter;

            Assert.AreEqual(1, actual.Count(), "Algorithm Eliminate did not return 8 changes");
            Assert.IsTrue(expectedK.SetEquals(actual));
            Assert.AreEqual(8, filter.Count, "Filter value incorrect");
            Assert.IsTrue(expectedV.SetEquals(filter), "Filter values incorrect");            
        }
    }
}
