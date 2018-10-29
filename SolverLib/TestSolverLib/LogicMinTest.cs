using SolverLib.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestSolverLib
{
    
    
    /// <summary>
    ///This is a test class for LogicMinTest and is intended
    ///to contain all LogicMinTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogicMinTest
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
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            LogicMin target = new LogicMin();
            LogicLeaf n1 = new LogicLeaf(new LogicResult(3));
            LogicLeaf n2 = new LogicLeaf(new LogicResult(5));
            LogicLeaf n3 = new LogicLeaf(new LogicResult(7));
            target.Add(n1);
            target.Add(n2);
            target.Add(n3);
            object data = null;
            ILogicStack stack = new LogicStack();
            target.Parse(data, stack);
            Assert.AreEqual(3, stack.Peek().Value.ToInt32, "Min failed");
        }
    }
}
