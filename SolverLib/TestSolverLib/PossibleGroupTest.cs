using System.Linq;
using SolverLib.Core.ValueGroup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Core;
using System.Collections.Generic;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for PossibleGroupTest and is intended
    ///to contain all PossibleGroupTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PossibleGroupTest
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
        public void DoesNotContainAnyTest()
        {
            
            IPossible p1 = new Possible() { 6,7,8,9,10 };
            IPossible p2 = new Possible() { 2, 3, 5, 8 };
            IPossible p3 = new Possible() { 3, 7, 10, 12 };
            ISetOfSets<IPossible> group1 = new SetOfSets<IPossible>() { p1, p2, p3 };

            IPossible q1 = new Possible() { 8, 3, 7, 22 };
            IPossible q2 = new Possible() { 2, 3, 8, 10 };
            ISetOfSets<IPossible> group2 = new SetOfSets<IPossible>() { q1, q2 };

            Assert.IsTrue(group1.DoesNotContainAny(group2), "Could not find 2,3,5,8");

            IPossible q3 = new Possible() { 5, 3, 2, 8 };
            group2.Add(q3);

            Assert.IsFalse(group1.DoesNotContainAny(group2), "Found 2,3,8,10");
        }


        [TestMethod()]
        public void ContainsPossibleTest()
        {
            IPossible find = new Possible() { 5, 2, 3, 8 };
            IPossible noFind = new Possible() { 2, 3, 8, 10 };

            IPossible p1 = new Possible() { 6, 7, 8, 9, 10 };
            IPossible p2 = new Possible() { 2, 3, 5, 8 };
            IPossible p3 = new Possible() { 3, 7, 10, 12 };
            ISetOfSets<IPossible> group = new SetOfSets<IPossible>() { p1, p2, p3 };

            Assert.IsTrue(group.Contains(find), "Could not find 2,3,5,8");
            Assert.IsFalse(group.Contains(noFind), "Found 2,3,8,10");
        }

 

        [TestMethod()]
        public void ContainsAllPossibleTest()
        {
            IPossible f1 = new Possible() { 2, 3, 5, 8 };
            IPossible f2 = new Possible() { 3, 7, 10, 12 };
            ISetOfSets<IPossible> group1 = new SetOfSets<IPossible>() { f1, f2 };

            IPossible p1 = new Possible() {2, 3, 5, 8};
            IPossible p2 = new Possible() {3, 7, 10, 12};
            IPossible p3 = new Possible() {6, 7, 8, 9, 10};
            ISetOfSets<IPossible> group2 = new SetOfSets<IPossible>() {p1, p2, p3};

            Assert.IsTrue(group2.ContainsAll(group1), "Could not find group1");

            IPossible f3 = new Possible() {6, 7, 8};
            group1.Add(f3);
            Assert.IsFalse(group2.ContainsAll(group1), "Found all group1");
            

        }
    }
}
