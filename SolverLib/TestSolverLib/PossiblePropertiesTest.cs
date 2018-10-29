using System.Linq;
using SolverLib.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SolverLib.Core.ValueGroup;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for PossiblePropertiesTest and is intended
    ///to contain all PossiblePropertiesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PossiblePropertiesTest
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
        ///A test for the method MinMaxPossible
        ///</summary>
        [TestMethod()]
        public void CheckMinMaxPossible()
        {

            IPossible l1 = new Possible() { 1, 2, 3, 4, 5 };
            IPossible l2 = new Possible() { 6, 7, 8, 9, 10 };
            IPossible l3 = new Possible() { 11, 12, 13, 14, 15 };
            IPossibleProperties<IPossible, int> prop = new PossibleProperties<IPossible, int>();
            prop.Add(l1, 5);
            prop.Add(l2, 10);
            prop.Add(l3, 15);

            IPossible p3 = new Possible() { 4, 7, 11, 13 };
            IPossibleProperties<IPossible, int> categorised = prop.Categorise(p3);
            Assert.AreEqual(15, categorised.Values.Max(), "Upper value incorrect");
            Assert.AreEqual(5, categorised.Values.Min(), "Lower value incorrect");
        }

                /// <summary>
        ///A test for Distribution
        ///</summary>
        [TestMethod()]
        public void CheckCategoryCount()
        {

            IPossible cat1 = new Possible() { 1, 2, 3, 4, 5 };
            IPossible cat2 = new Possible() { 6, 7, 8, 9, 10 };
            IPossible cat3 = new Possible() { 11, 12, 13, 14, 15 };
            IList<IPossible> categories = new List<IPossible>(){cat1,cat2,cat3};

            IPossible p1 = new Possible() { 4, 7, 11, 13 };
            IPossible p2 = new Possible() { 2, 3, 5, 8 };
            IPossible p3 = new Possible() { 3, 7, 10, 12 };
            ISetOfSets<IPossible> group = new SetOfSets<IPossible>(){p1,p2,p3};

            IPossibleProperties<IPossible,int> count = group.CategoryCount(categories);
            Assert.AreEqual(3, count[cat1]);
            Assert.AreEqual(3, count[cat2]);
            Assert.AreEqual(2, count[cat3]);
        }

        /// <summary>
        ///A test for Distribution
        ///</summary>
        [TestMethod()]
        public void ListGroupSortingTest()
        {

            IList<int> cat1 = new List<int>() {1, 2, 3, 4, 5};
            IList<int> cat2 = new List<int>() { 6, 7, 8, 9, 10 };
            IList<int> cat3 = new List<int>() { 11, 12, 13, 14, 15 };
            IList<IList<int>> qualifiers = new List<IList<int>>() {cat1, cat2, cat3};
            
            IList<int> p1 = new List<int>() {4, 7, 11, 13};
            IList<int> p2 = new List<int>() { 2, 3, 5, 8 };
            IList<int> p3 = new List<int>() { 3, 7, 10, 12 };
            ISetOfLists<IList<int>> group = new SetOfLists<IList<int>>() { p1, p2, p3 };

            IPossibleProperties<IList<int>, int> count = group.CategoryCount(qualifiers);
            List<int> list = new List<int>(count.Values);
            list.Sort();
            list.ToArray();
        }
    }
}
