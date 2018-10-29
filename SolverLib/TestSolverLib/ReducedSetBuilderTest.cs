using System.Linq;
using SolverLib.Algorithms;
using SolverLib.Constraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.SetTesters;
using SolverLib.Space;
using SolverLib.Core;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for ReducedSetBuilderTest and is intended
    ///to contain all ReducedSetBuilderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReducedSetBuilderTest
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
        ///A test for Do
        ///</summary>
        public void ReducedSetTestHelper<TKey>()
        {
            ISpace<int> space = new Space<int>(new Possible(){1,2,3,4,5,6,7,8,9});
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible(){1,2,3,4,5,6,7,8,9});
            }
            Keys<int> k1 = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7 };
            Keys<int> k2 = new Keys<int>() { 1, 2, 3, 4, 5, 6, 9 };
            Keys<int> k3 = new Keys<int>() { 1, 2, 3, 4, 5, 8, 9 };
            Keys<int> k4 = new Keys<int>() { 1, 2, 3, 4, 7, 8, 9 };
            Keys<int> k5 = new Keys<int>() { 1, 2, 3, 6, 7, 8, 9 };
            foreach (int i in k1)
            {
                space[i].Values.Remove(1);
            }
            foreach (int i in k2)
            {
                space[i].Values.Remove(2);
            }
            foreach (int i in k3)
            {
                space[i].Values.Remove(3);
            }
            foreach (int i in k4)
            {
                space[i].Values.Remove(4);
            }
            foreach (int i in k5)
            {
                space[i].Values.Remove(5);
            }
            

            Keys<int> subset = new Keys<int>(){7,8};
            Keys<int> set = new Keys<int>(){1,2,3,4,5,6,7,8,9};
            // This test will create 5 reduced sets. The outer keys of k1-k5
            ReducedSetTester<int> tester = new ReducedSetTester<int>(set, space);          
            bool actual = tester.ReducedSet(subset);
            Assert.AreEqual(true, actual, "Did not find reduced set");
            Assert.AreEqual(1, tester.Regions.Count, "Regions not equal to 1");
            Assert.AreEqual(2, tester.Regions.First().Keys.Count, "Reduced Set Does not contain correct Key count");
            Assert.AreEqual(1, tester.Regions.First().Value.Count, "Region does not contain a value");
            Assert.AreEqual(2, tester.Regions.First().Value.First(), "Region does not contain correct value");
        }

        [TestMethod()]
        public void ReducedSetTest()
        {
            ReducedSetTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Do
        ///</summary>
        public void ReducedSetNegTestHelper<TKey>()
        {
            ISpace<int> space = new Space<int>(new Possible() {1, 2, 3, 4, 5, 6, 7, 8, 9});
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() {1, 2, 3, 4, 5, 6, 7, 8, 9});
            }

            
            Keys<int> subset = new Keys<int>() {8, 9};
            Keys<int> set = new Keys<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
            ReducedSetTester<int> target = new ReducedSetTester<int>(set, space);
            bool actual = target.ReducedSet(subset);
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void ReducedSetValues12Test()
        {
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            space[1].SetValue(1);
            space[2].SetValue(2);

            
            Keys<int> subset = new Keys<int>() { 1,2 };
            Keys<int> set = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ReducedSetTester<int> target = new ReducedSetTester<int>(set, space); 
            bool actual = target.ReducedSet(subset);
            // Have not eliminated 1 and 2 from remaining set so will fail
            Assert.AreEqual(false, actual);
            //Assert.AreEqual(2, target.SetProcess.Regions.First().Leaf.Count);

        }

        [TestMethod()]
        public void ReducedSetNegTest()
        {
            ReducedSetNegTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Do
        ///</summary>
        public void ReducedSetFinderTestHelper<TKey>()
        {
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            Keys<int> k1 = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7 };
            Keys<int> k2 = new Keys<int>() { 1, 2, 3, 4, 5, 6, 9 };
            Keys<int> k3 = new Keys<int>() { 1, 2, 3, 4, 5, 8, 9 };
            Keys<int> k4 = new Keys<int>() { 1, 2, 3, 4, 7, 8, 9 };
            Keys<int> k5 = new Keys<int>() { 1, 2, 3, 6, 7, 8, 9 };
            foreach (int i in k1)
            {
                space[i].Values.Remove(2);
            }
            foreach (int i in k2)
            {
                space[i].Values.Remove(3);
            }
            foreach (int i in k3)
            {
                space[i].Values.Remove(4);
            }
            foreach (int i in k4)
            {
                space[i].Values.Remove(5);
            }
            foreach (int i in k5)
            {
                space[i].Values.Remove(6);
            }

            Keys<int> wholeSet = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ReducedSetTester<int> tester = new ReducedSetTester<int>(wholeSet, space);
            CombinationValues<int> combi = new CombinationValues<int>(2, wholeSet);
            combi.Test = tester.ReducedSet;
            combi.Store = false;
            combi.CalcCombinations();

            int iCount = tester.Regions.Count();
            Assert.AreEqual(5, iCount, "Did not find correct number of Reduced Sets");
            Assert.AreEqual(2, tester.Regions.First().Keys.Count, "Reduced Set does not contain two keys");
            Assert.AreEqual(6, tester.Regions.First().Value.First(), "Reduced Set value is not correct");
            Assert.AreEqual(4, tester.Regions.First().Keys.First(), "Reduced Set key1 is not correct");
            Assert.AreEqual(5, tester.Regions.First().Keys.Last(), "Reduced Set key2 is not correct");
            //Assert.AreEqual(1, process.Regions.Count, "Regions not equal to 1");
            //Assert.AreEqual(1, process.Regions.First().Leaf.Count, "Region does not contain a value");
            //Assert.AreEqual(6, process.Regions.First().Leaf.First(), "Region does not contain the value 6");
        }


        [TestMethod()]
        public void ReducedSetFinderTest()
        {
            ReducedSetFinderTestHelper<GenericParameterHelper>();
        }
    }
}
