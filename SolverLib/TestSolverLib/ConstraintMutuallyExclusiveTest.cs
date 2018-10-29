using SolverLib.Job;
using SolverLib.Constraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Core;
using SolverLib.Engine;
using SolverLib.Puzzle;
using SolverLib.Reader;
using SolverLib.Space;
using System.Collections.Generic;
using System.Linq;

namespace TestSolverLib
{
    // Accessor class
    public class ConstraintTester<TKey> : ConstraintMutuallyExclusive<TKey>
    {
        public ConstraintTester(string name, Keys<TKey> keys) : base(name, keys)
        {
            
        }

        public void CreateActions1(Keys<TKey> keysChanged, IPuzzleEngine<TKey> engine)
        {
            base.CreateActions1(keysChanged, engine);
        }

        public void CreateActions2(Keys<TKey> keysChanged, IPuzzleEngine<TKey> engine)
        {
            base.CreateActions2(keysChanged, engine);
        }

        public void CreateActions3(Keys<TKey> keysChanged, IPuzzleEngine<TKey> engine)
        {
            base.CreateActions3(keysChanged, engine);
        }
    }
    
    /// <summary>
    ///This is a test class for ConstraintMutuallyExclusiveTest and is intended
    ///to contain all ConstraintMutuallyExclusiveTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConstraintMutuallyExclusiveTest
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
        ///A test to check when a key is set with one value
        /// it eliminates this value from the outer set
        ///</summary>
        [TestMethod()]
        public void CreateActionTestWith1()
        {
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            Keys<int> group = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstraintMutuallyExclusive<int> target = new ConstraintMutuallyExclusive<int>("row1", group);
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            space[9].SetValue(1);
            Keys<int> keysChangedIn = new Keys<int>() { 9 };
            int jobsAdded = target.CreateSearchJobs(keysChangedIn, engine);
            Assert.AreEqual(1, jobsAdded, "Incorrect Jobs added");

            // Test
            Possible expectedFilter = new Possible() { 1 };
            Keys<int> expectedKeys = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };   
            IJobFilter<int> nextJob = engine.Peek() as IJobFilter<int>;
            Assert.IsTrue(nextJob.Keys.SetEquals(expectedKeys), "ConstraintME did not change the correct keys");
            Assert.IsTrue(nextJob.Filter.SetEquals(expectedFilter), "ConstraintME did not change the correct keys");
        }

        /// <summary>
        ///A test for 2 known values in one ME group
        ///</summary>
        [TestMethod()]
        public void CreateActionTestWith2x2()
        {
            // Setup
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            Keys<int> group = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstraintMutuallyExclusive<int> target = new ConstraintMutuallyExclusive<int>("row1", group);

            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            puzzle.Constraints.Add(target);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            space[8].SetValues(new Possible() { 1, 2 });
            space[9].SetValues(new Possible() { 1, 2 });
            Keys<int> keysChangedIn = new Keys<int>() { 8,9 };
            target.CreateSearchJobs(keysChangedIn, engine);

            // Tests
            Possible expectedFilter = new Possible() { 1,2 };
            Keys<int> expectedKeys = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7 };
            Assert.AreEqual(1, engine.Count, "Incorrect Jobs added");
            IJobFilter<int> nextJob = engine.Peek() as IJobFilter<int>;
            Assert.IsTrue(nextJob.Keys.SetEquals(expectedKeys), "ConstraintME did not change the correct keys");
            Assert.IsTrue(nextJob.Filter.SetEquals(expectedFilter), "ConstraintME did not set the correct values");
            
        }

        /// <summary>
        ///A test for checking a complete set (7,8,9) is found 
        /// and it removes the values (7,8,9) from the outer set (1,2,3,4,5,6)
        ///</summary>
        [TestMethod()]
        public void CreateActionTestWith232()
        {
            // Setup
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            Keys<int> group = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstraintMutuallyExclusive<int> target = new ConstraintMutuallyExclusive<int>("row1", group);
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            puzzle.Constraints.Add(target);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            space[7].SetValues(new Possible() { 1, 2 });
            space[8].SetValues(new Possible() { 1, 2, 3 });
            space[9].SetValues(new Possible() { 2, 3 });
            Keys<int> keysChangedIn = new Keys<int>() { 7, 8, 9 };
            target.CreateSearchJobs(keysChangedIn, engine);

            // Tests
            Possible expectedFilter = new Possible() {1, 2, 3};
            Keys<int> expectedKeys = new Keys<int>() { 1, 2, 3, 4, 5, 6 };
            Assert.AreEqual(1, engine.Count, "Incorrect Jobs added");
            IJobFilter<int> nextJob = engine.Peek() as IJobFilter<int>;
            Assert.IsTrue(nextJob.Keys.SetEquals(expectedKeys), "ConstraintME did not change the correct keys");
            Assert.IsTrue(nextJob.Filter.SetEquals(expectedFilter), "ConstraintME did not set the correct values");
        }

        /// <summary>
        /// Another test for checking a complete set (7,8,9) is found 
        /// and it removes the values (7,8,9) from the outer set (1,2,3,4,5,6)
        ///</summary>
        [TestMethod()]
        public void CreateActionTestWith3x2()
        {
            // Setup
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            Keys<int> group = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstraintMutuallyExclusive<int> target = new ConstraintMutuallyExclusive<int>("row1",group);
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            puzzle.Constraints.Add(target);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            space[7].SetValues(new Possible() { 1, 2 });
            space[8].SetValues(new Possible() { 2, 3 });
            space[9].SetValues(new Possible() { 1, 3 });
            Keys<int> keysChangedIn = new Keys<int>() { 7, 8, 9 };
            target.CreateSearchJobs(keysChangedIn, engine);

            // Tests
            Possible expectedFilter = new Possible() { 1, 2, 3 };
            Keys<int> expectedKeys = new Keys<int>() { 1, 2, 3, 4, 5, 6 };
            Assert.AreEqual(1, engine.Count, "Incorrect Jobs added");
            IJobFilter<int> nextJob = engine.Peek() as IJobFilter<int>;
            Assert.IsTrue(nextJob.Keys.SetEquals(expectedKeys), "ConstraintME did not change the correct keys");
            Assert.IsTrue(nextJob.Filter.SetEquals(expectedFilter), "ConstraintME did not set the correct values");
            
        }

        /// <summary>
        /// A test to resolve the last place a nine can go.
        /// Space[1] contains 1-9 all others contain 1-8
        /// </summary>
        [TestMethod()]
        public void CreateActionTestWith8x8()
        {
            // Setup
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            Keys<int> group = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ConstraintMutuallyExclusive<int> target = new ConstraintMutuallyExclusive<int>("row1", group);
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            puzzle.Constraints.Add(target);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            Keys<int> changeKeys = new Keys<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int k in changeKeys)
            {
                space[k].SetValues(new Possible() {1, 2, 3, 4, 5, 6, 7, 8});
            }
            Keys<int> keysChangedIn = new Keys<int>() { 1,2,3,4,5,6,7,8 };
            target.CreateSearchJobs(keysChangedIn, engine);

            // Tests
            Possible expectedFilter = new Possible() { 1,2,3,4,5,6,7,8 };
            Keys<int> expectedKeys = new Keys<int>() { 1};
            Assert.AreEqual(1, engine.Count, "Incorrect Jobs added");
            IJobFilter<int> nextJob = engine.Peek() as IJobFilter<int>;
            
            Assert.IsTrue(nextJob.Keys.SetEquals(expectedKeys), "ConstraintME did not change the correct keys");
            Assert.IsTrue(nextJob.Filter.SetEquals(expectedFilter), "ConstraintME did not set the correct values");
            
        }



        /// <summary>
        /// A test to check the Intersect Constraint works
        /// The intersection contains 3 and it is not in the outer set of constraint1
        /// Therefore it cannot be in the outer set of constraint2
        ///</summary>
        [TestMethod()]
        public void CreateActions2Test()
        {
            // Setup
            Keys<int> group1In = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Keys<int> group2In = new Keys<int>() {10, 11, 12, 13, 14, 15, 16, 17, 18};
            Keys<int> group3In = new Keys<int>() { 1, 2, 3, 10, 11, 12, 19, 20, 21 };
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 22; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }

            ConstraintTester<int> target1 = new ConstraintTester<int>("row1", group1In);
            ConstraintMutuallyExclusive<int> target2 = new ConstraintMutuallyExclusive<int>("row2", group2In);
            ConstraintMutuallyExclusive<int> target3 = new ConstraintMutuallyExclusive<int>("grid1" ,group3In);

            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            puzzle.Constraints.Add(target1);
            puzzle.Constraints.Add(target2);
            puzzle.Constraints.Add(target3);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            Possible no3 = new Possible() {1, 2, 4, 5, 6, 7, 8, 9};
            Keys<int> keysOuter1 = new Keys<int>(){4,5,6,7,8,9};
            foreach (int k in keysOuter1)
            {
                space[k].SetValues(no3);
            }
            target1.CreateActions2(keysOuter1, engine);

            // Tests
            Possible expectedFilter = new Possible(){3};
            Keys<int> expectedKeys = new Keys<int>() {10, 11, 12, 19, 20, 21};
            IJobFilter<int> nextJob = engine.Peek() as IJobFilter<int>;
            Assert.AreEqual(1, engine.Count, "Incorrect Jobs added");
            Assert.IsTrue(nextJob.Keys.SetEquals(expectedKeys), "ConstraintME did not change the correct keys");
            Assert.IsTrue(nextJob.Filter.SetEquals(expectedFilter), "ConstraintME did not set the correct values");
        }

        protected IConstraint<int> GetConstraintTester(Keys<int> keys)
        {
            IConstraint<int> constraintTester = new ConstraintTester<int>("", keys);
            return constraintTester;
        }

        /// <summary>
        ///A test for DetectXWing
        ///</summary>
        [TestMethod()]
        public void CreateXWingActionsTest()
        {
            // Setup
            Keys<int> group1In = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Keys<int> group2In = new Keys<int>() { 1, 10, 19, 28, 37, 46, 55, 64, 73 };
            Keys<int> group3In = new Keys<int>() { 9, 18, 27, 36, 45, 54, 63, 72, 81};
            Keys<int> group4In = new Keys<int>() {28, 29, 30, 31, 32, 33, 34, 35, 36};
            Keys<int> group5In = new Keys<int>() { 1, 2, 3, 10, 11, 12, 19, 20, 21 };
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 82; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            ConstraintTester<int> target1 = new ConstraintTester<int>("row1", group1In);
            ConstraintMutuallyExclusive<int> target2 = new ConstraintMutuallyExclusive<int>("col1", group2In);
            ConstraintMutuallyExclusive<int> target3 = new ConstraintMutuallyExclusive<int>("col9", group3In);
            ConstraintMutuallyExclusive<int> target4 = new ConstraintMutuallyExclusive<int>("row4", group4In);
            ConstraintMutuallyExclusive<int> target5 = new ConstraintMutuallyExclusive<int>("grid1", group5In);
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            puzzle.Constraints.Add(target1);
            puzzle.Constraints.Add(target2);
            puzzle.Constraints.Add(target3);
            puzzle.Constraints.Add(target4);
            puzzle.Constraints.Add(target5);
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            // Action
            Keys<int> keysChanged = new Keys<int>() { 2, 3, 4, 5, 6, 7, 8, 29, 30, 31, 32, 33, 34, 35 };
            foreach (int k in keysChanged)
            {
                space[k].Values.Remove(2);
            }
            target1.CreateActions3(keysChanged, engine);
 
            // Test
            Possible origValue = new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Possible expectedFilter = new Possible(){2};
            
            Assert.AreEqual(2, engine.Count, "Two X wing sets not found");
            IJobFilter<int> job1 = engine.ElementAt(0) as IJobFilter<int>;
            IJobFilter<int> job2 = engine.ElementAt(1) as IJobFilter<int>;
            //Keys<int> kExpected11 = new Keys<int>() {1, 28};
            //Keys<int> kExpected12 = new Keys<int>() {9, 36};
            Keys<int> kExpected21 = new Keys<int>() { 10, 19, 37, 46, 55, 64, 73 };
            Keys<int> kExpected22 = new Keys<int>() { 18, 27, 45, 54, 63, 72, 81 };
            Keys<int> kNotExpected2 = new Keys<int>() {11, 12, 20, 21 };
            Assert.IsTrue(job1.Keys.SetEquals(kExpected21));
            Assert.IsTrue(job1.Filter.SetEquals(expectedFilter));
            Assert.IsTrue(job2.Keys.SetEquals(kExpected22));
            Assert.IsTrue(job2.Filter.SetEquals(expectedFilter));
        }

        /// <summary>
        ///A test for DetectXWing
        ///</summary>
        [TestMethod()]
        public void CreateXWingActionsHardTest()
        {
            // Setup
            Keys<int> row1 = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Keys<int> col1 = new Keys<int>() { 1, 10, 19, 28, 37, 46, 55, 64, 73 };
            Keys<int> col3 = new Keys<int>() { 3, 12, 21, 30, 39, 48, 57, 66, 75 };
            Keys<int> col9 = new Keys<int>() { 9, 18, 27, 36, 45, 54, 63, 72, 81 };
            Keys<int> row3 = new Keys<int>() { 19, 20, 21, 22, 23, 24, 25, 26, 27 };
            Keys<int> row4 = new Keys<int>() { 28, 29, 30, 31, 32, 33, 34, 35, 36 };
            Keys<int> grid1 = new Keys<int>() { 1, 2, 3, 10, 11, 12, 19, 20, 21 };
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 82; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            Keys<int> reducedSet1 = new Keys<int>() {2,3,4,5,6,7,8,9 };
            foreach (int k in reducedSet1)
            {
                space[k].Values.Remove(2);
            }
            Keys<int> reducedSet2 = new Keys<int>(){2,10,11,12,19,20};
            foreach (int k in reducedSet2)
            {
                space[k].Values.Remove(2);
            }
            
            ConstraintMutuallyExclusive<int> cGrid1 = new ConstraintMutuallyExclusive<int>("grid1", grid1);
            ConstraintTester<int> cCol1 = new ConstraintTester<int>("col1", col1);
            ConstraintMutuallyExclusive<int> cCol3 = new ConstraintMutuallyExclusive<int>("col3", col3);
            ConstraintMutuallyExclusive<int> cCol9 = new ConstraintMutuallyExclusive<int>("col9", col9);
            ConstraintTester<int> cRow1 = new ConstraintTester<int>("row1", row1);
            ConstraintTester<int> cRow3 = new ConstraintTester<int>("row3", row3);
            
            IPuzzle<int> puzzle = new PuzzleBase<int>(space);
            puzzle.Constraints.Add(cGrid1);
            puzzle.Constraints.Add(cCol1);
            puzzle.Constraints.Add(cCol3);
            puzzle.Constraints.Add(cCol9);
            puzzle.Constraints.Add(cRow1);
            puzzle.Constraints.Add(cRow3);
            
            IPuzzleEngine<int> engine = new PuzzleEngine<int>(puzzle);

            //SpaceAdapter<int>.ToFile(puzzle.Space.ToString(),0);
            // Action
            cRow1.CreateActions3(reducedSet1, engine);
            //SpaceAdapter<int>.ToFile(puzzle.Space.ToString(),1);
            Assert.AreEqual(0, engine.Count, "Two X wing sets not found");
            
        }

        
    }
}
