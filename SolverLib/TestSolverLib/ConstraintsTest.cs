using System.Linq;
using SolverLib.Constraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Core;
using System.Collections.Generic;

namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for ConstraintsTest and is intended
    ///to contain all ConstraintsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConstraintsTest
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
        ///A test for FindOtherConstraintsContainingAllKeys
        ///</summary>
        public void FindOtherConstraintsContainingAllKeysTestHelper<TKey>()
        {
            IConstraints<int> constraints = new Constraints<int>();
            Keys<int> group1 = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IConstraint<int> constraint1 = new ConstraintMutuallyExclusive<int>("row1", group1);
            Keys<int> group2 = new Keys<int>() {1, 10, 19, 28, 37, 46, 55, 64, 73};
            IConstraint<int> constraint2 = new ConstraintMutuallyExclusive<int>("col1", group2);
            Keys<int> group3 = new Keys<int>() { 1, 2, 3, 10, 11, 12, 19, 20, 21 };
            IConstraint<int> constraint3 = new ConstraintMutuallyExclusive<int>("grid1", group3);
            constraints.Add(constraint1);
            constraints.Add(constraint2);
            constraints.Add(constraint3);

            // Calling Params
            Keys<int> keys = new Keys<int>(){1};
            ICollection<ConstraintType> types = new List<ConstraintType>(){ConstraintType.MutuallyExclusive};
            IConstraints<int> excluding = new Constraints<int>() {constraint1};
            IConstraints<int> expected = new Constraints<int>() { constraint2, constraint3 };
            IConstraints<int> actual = constraints.FindOtherConstraintsContainingAllKeys(keys, types, excluding);
            Assert.AreEqual(expected.Count, actual.Count, "Incorrect count");
            Assert.AreEqual(actual.ElementAt(0).Name, constraint2.Name, "First element does not equal col1");
            Assert.AreEqual(actual.ElementAt(1).Name, constraint3.Name, "Second element does not equal grid1");
        }

        [TestMethod()]
        public void FindOtherConstraintsContainingAllKeysTest()
        {
            FindOtherConstraintsContainingAllKeysTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for FindOtherConstraintsContainingAllKeys
        ///</summary>
        [TestMethod()]
        public void FindOtherConstraintsContainingAllKeysTest2()
        {
            IConstraints<int> constraints = new Constraints<int>();
            Keys<int> group1 = new Keys<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IConstraint<int> constraint1 = new ConstraintMutuallyExclusive<int>("row1", group1);
            Keys<int> group2 = new Keys<int>() { 1, 10, 19, 28, 37, 46, 55, 64, 73 };
            IConstraint<int> constraint2 = new ConstraintMutuallyExclusive<int>("col1", group2);
            Keys<int> group3 = new Keys<int>() { 1, 2, 3, 10, 11, 12, 19, 20, 21 };
            IConstraint<int> constraint3 = new ConstraintMutuallyExclusive<int>("grid1", group3);
            Keys<int> group4 = new Keys<int>() { 2, 11, 20, 29, 38, 47, 56, 65, 74 };
            IConstraint<int> constraint4 = new ConstraintMutuallyExclusive<int>("col2", group4);
            
            constraints.Add(constraint1);
            constraints.Add(constraint2);
            constraints.Add(constraint3);
            constraints.Add(constraint4);

            // Calling Params
            Keys<int> keys = new Keys<int>() { 1,2 };
            ICollection<ConstraintType> types = new List<ConstraintType>() { ConstraintType.MutuallyExclusive };
            IConstraints<int> excluding = new Constraints<int>();
            IConstraints<int> expected = new Constraints<int>() { constraint1, constraint3 };
            IConstraints<int> actual = constraints.FindOtherConstraintsContainingAllKeys(keys, types, excluding);
            Assert.AreEqual(expected.Count, actual.Count, "Incorrect count");
            Assert.AreEqual(actual.ElementAt(0).Name, constraint1.Name, "First element does not equal col1");
            Assert.AreEqual(actual.ElementAt(1).Name, constraint3.Name, "Second element does not equal grid1");
        }

    }
}
