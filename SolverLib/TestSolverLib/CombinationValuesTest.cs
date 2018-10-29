using System;
using SolverLib.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for CombinationValuesTest and is intended
    ///to contain all CombinationValuesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CombinationValuesTest
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

        [TestMethod()]
        public void ProcessTest()
        {
            int size = 4; 
            IList<int> values = new List<int>(){1,2,3,4,5,6}; // TODO: Initialize to an appropriate value
            CombinationValues<int> target = new CombinationValues<int>(size, values); 
            int combinations = target.CalcCombinations();
            Assert.AreEqual(15, combinations, "Wrong number of combinations returned");
            foreach (IList<int> list in target.Combinations)
            {
                string s = string.Empty;
                if (list.Count > 0)
                {
                    s += list[0];
                }
                for (int i = 1; i < list.Count;i++ )
                {
                    s += "," + list[i];
                }
                Console.WriteLine(s);
            }

        }

        
    
    }
}
