using System.Collections.Generic;
using SolverLib.Algorithms;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Space;
using System.Linq;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for PuzzleConstraintsTest and is intended
    ///to contain all PuzzleConstraintsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PuzzleConstraintsTest
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
        ///A test for CreateSearchJobs
        ///</summary>
        public void ApplyConstraintsTestHelper<TKey>()
        {

            // Initialise Space
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 82; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }

            IConstraints<int> constraints = new Constraints<int>();
            Keys<int> group = new Keys<int>(){1,2,3,4,5,6,7,8,9};
            IConstraint<int> constraint = new ConstraintMutuallyExclusive<int>("row1", group);
            Keys<int> group2 = new Keys<int>(){1,2,3,10,11,12,19,20,21};

            IConstraint<int> constraint2 = new ConstraintMutuallyExclusive<int>("grid1", group2);
            constraints.Add(constraint);
            constraints.Add(constraint2);

            IPuzzle<int> puzzle = new PuzzleBase<int>();
            puzzle.Space = space;
            puzzle.Constraints = constraints;
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            ISpace<int> init = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            init[1] = new Possible(){1};
            engine.SetInitialValues(init);
            Assert.AreEqual(1, engine.Count, "Constraint not added to queue");
            engine.DoNextJob();
            // this brings back keys 2 and 3 with constraint grid1? is
            Assert.AreEqual(2, engine.Count, "MutuallyExclusive constraint did not create jobs");
       
          
        }

        [TestMethod()]
        public void ApplyConstraintsTest()
        {
            ApplyConstraintsTestHelper<GenericParameterHelper>();
        }
    }
}
