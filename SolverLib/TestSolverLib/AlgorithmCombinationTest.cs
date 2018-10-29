using System.Collections.Generic;
using System.Linq;
using SolverLib.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Constraints;
using SolverLib.Core;
using SolverLib.SetTesters;
using SolverLib.Space;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for AlgorithmCombinationTest and is intended
    ///to contain all AlgorithmCombinationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AlgorithmCombinationTest
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
        ///A test for GetSetCombinations
        ///</summary>
        public void GetSetCombinationsTestHelper<TKey>()
        {
            
            Keys<int> set = new Keys<int>() {2, 4, 6, 8, 10, 12, 14, 16, 18};
            CombinationValues<int> combi = new CombinationValues<int>(8, set);
            Assert.AreEqual(9, combi.CalcCombinations(), "Did not produce 9 combinations");
        }

        [TestMethod()]
        public void GetReducedSetTest()
        {
            ISpace<int> space = new Space<int>(new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 1; i < 10; i++)
            {
                space.Add(i, new Possible() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            for (int i = 1; i < 8; i++)
            {
                space[i].Values.Remove(6);
            }
            space[1].Values.Remove(1);
            space[2].Values.Remove(3);
            space[3].Values.Remove(5);
            Keys<int> mySet = new Keys<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};

            Keys<int> expected = new Keys<int>() {8, 9};

            ReducedSetTester<int> tester = new ReducedSetTester<int>(mySet, space);
            CombinationValues<int> combi = new CombinationValues<int>(2, mySet);
            combi.Store = false;
            combi.Test = tester.ReducedSet;
            int combinations = combi.CalcCombinations();
            Assert.AreEqual(1, tester.Regions.Count);
            Assert.IsTrue(expected.SetEquals(tester.Regions.First().Keys), "Reduced Set found does not equal 8,9");
             
        }

        [TestMethod()]
        public void GetReducedSetFalseTest()
        {
            ISpace<int> space = new Space<int>(new Possible() {1, 2, 3, 4, 5, 6, 7, 8, 9});
            for (int i = 1; i < 82; i++)
            {
                space.Add(i, new Possible() {1, 2, 3, 4, 5, 6, 7, 8, 9});
            }
            space[1].SetValue(1);
            space[2].SetValue(2);
            space[3].SetValue(3);
            space[7].SetValue(7);
            space[8].SetValue(8);
            space[9].SetValue(9);
            space[4].Remove(6);
            space[5].Remove(6);
            space[13].Remove(6);
            space[14].Remove(6);
            space[22].Remove(6);
            space[23].Remove(6);
            space[4].Remove(5);
            space[13].Remove(5);
            space[22].Remove(5);
            Keys<int> mySet = new Keys<int>() {4, 5, 6, 13, 14, 15, 22, 23, 24};

            Keys<int> expected = new Keys<int>() { 6, 15, 9 };

            ReducedSetTester<int> tester = new ReducedSetTester<int>(mySet, space);
            CombinationValues<int> combi = new CombinationValues<int>(2, mySet);
            int combinations = combi.CalcCombinations();
            Assert.AreEqual(0, tester.Regions.Count);
            //Assert.IsTrue(expected.SetEquals(setProcess.Regions.First().Keys), "Reduced Set found does not equal 8,9");
        }

        [TestMethod()]
        public void GetSetCombinationsTest()
        {
            GetSetCombinationsTestHelper<GenericParameterHelper>();
        }
    }
}
