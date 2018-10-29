using SolverLib.Job;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using SolverLib.Space;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for ProcessIntersectionEliminateTest and is intended
    ///to contain all ProcessIntersectionEliminateTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProcessIntersectionEliminateTest
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
        ///A test for Process
        ///</summary>
        public void ProcessIntersectionEliminateProcessTestHelper<TKey>()
        {
            Keys<int> group1In = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Keys<int> group2In = new Keys<int>() {1, 2, 3, 10, 11, 12, 19, 20, 21};
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 22; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            // remove 1 from part of row1
            for (int i = 4; i < 10; i++)
            {
                space[i].Values.Remove(1);
            }
            space[1].Values.Remove(5);
            space[2].Values.Remove(5);
            space[3].Values.Remove(5);
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            int jobsAdded = ConstraintMutuallyExclusive<int>.CreateCompleteIntersectActions(group1In, group2In, engine);

            // Tests
            Assert.AreEqual(1, jobsAdded, "Incorrect filter job count");
            Keys<int> expected = new Keys<int>() { 10, 11, 12, 19, 20, 21 };
            IJobFilter<int> job = engine.Peek() as IJobFilter<int>;
            Assert.IsNotNull(job);
            Assert.AreEqual(6, job.Keys.Count, "Unexpected key change.");
            Assert.IsTrue(expected.SetEquals(job.Keys), "Unexpected Keys Changed. Should be 10,11,12,19,20,21");
            Possible expectedValues = new Possible(){1};
            Assert.IsTrue(expectedValues.SetEquals(job.Filter), "Unexpected values. Should have eliminated 1");
        }

        [TestMethod()]
        public void ProcessIntersectionEliminateProcessTest()
        {
            ProcessIntersectionEliminateProcessTestHelper<GenericParameterHelper>();
        }
    }
}
