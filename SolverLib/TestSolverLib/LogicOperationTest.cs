using System;
using SolverLib.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for LogicOperationTest and is intended
    ///to contain all LogicOperationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogicOperationTest
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
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            LogicOperation target = new LogicOperation("Add");
            target.Add(new LogicOperation("3"));
            target.Add(new LogicOperation("3"));
            target.Add(new LogicOperation("Add"){new LogicOperation("4"), new LogicOperation("5")});
            string actual = target.ToString();
            Console.WriteLine(actual);
            Assert.AreEqual("Add(3[],3[],Add(4[],5[])[])[]", actual);
        }
    }
}
