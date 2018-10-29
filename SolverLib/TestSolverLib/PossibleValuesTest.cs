using System.Collections.Generic;
using SolverLib.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ModuleTests
{
    
    
    /// <summary>
    ///This is a test class for PossibleValuesTest and is intended
    ///to contain all PossibleValuesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PossibleValuesTest
    {


        private TestContext testContextInstance;

        private bool eventCalled = false;

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
        ///A test for FilterOut
        ///</summary>
        [TestMethod()]
        public void FilterOutTest()
        {
            // Start with 4-9. Filter out 3-5. Return 4-5
            Possible target = new Possible(){4,5,6,7,8,9}; 
            Possible filter = new Possible(){3,4,5};
            Possible expected = new Possible(){6,7,8,9};
            bool returnValue = target.FilterOut(filter);

            Assert.IsTrue(returnValue, "Filter Return value is false");
            Assert.AreEqual(4, target.Values.Count, "Filter Count is not 4");
            Assert.IsTrue(((HashSet<int>)target.Values).SetEquals(expected), "Filter operation failed expected output");   
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        [TestMethod()]
        public void SetTest()
        {
            // Start with 4-6. Set 4. Return true
            Possible target = new Possible() { 4, 5, 6 };
            Possible expected = new Possible() {4};
            bool returnValue = target.SetValue(4);

            Assert.AreEqual(1, target.Values.Count, "Set Count is not 1");
            Assert.IsTrue(target.SetEquals(expected), "Set with integer not expected value");
            Assert.IsTrue(returnValue, "Set return value is not true");
        }

        /// <summary>
        ///A test for SetValues
        ///</summary>
        [TestMethod()]
        public void SetValuesTest()
        {
            // Start with 4-6. Set 4. Return 5-6
            Possible target = new Possible() { 4, 5, 6 };
            Possible expected = new Possible(){4};
            bool returnValue = target.SetValues(expected);

            Assert.AreEqual(1, target.Values.Count, "Set Count is not 1");
            Assert.IsTrue(target.SetEquals(expected), "Set with integer not expected value");
            Assert.IsTrue(returnValue, "Set return value is not true");
        }

        [TestMethod]
        public void PropertyChangedTest()
        {
            Possible target = new Possible() { 4, 5, 6 };
            Possible expected = new Possible() { 2, 3, 4, 5, 6 };
            target.PropertyChanged += target_PropertyChanged;
            target.UnionPossible(new Possible(){2,3});
            Assert.IsTrue(expected.SetEquals(target), "Set Union failed");
            Assert.IsTrue(eventCalled, "Property Changed Event not called");
        }

        void target_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            eventCalled = true;
        }
    }
}
