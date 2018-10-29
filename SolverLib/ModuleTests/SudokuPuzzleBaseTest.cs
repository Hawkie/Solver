using SolverLib.Core;
using SolverModules.SamauraiSudoku;
using SolverModules.Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolverLib.Constraints;
using System.Linq;

namespace ModuleTests
{
    
    
    /// <summary>
    ///This is a test class for SudokuPuzzleBaseTest and is intended
    ///to contain all SudokuPuzzleBaseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SudokuPuzzleBaseTest
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
        ///A test for AddRows
        ///</summary>
        [TestMethod()]
        public void AddRowsTest()
        {
            SudokuPuzzleBase target = CreateSudokuPuzzleBase();
            Keys<int> expected = new Keys<int>() { 265, 266, 267, 268, 269, 270, 271, 272, 273 };
            int xOffset = 12;
            int yOffset = 12;
            int xSize = 9;
            int ySize = 9;
            int xWidth = 21;
            IConstraints<int> constraints = new Constraints<int>();
            target.AddRows(xOffset, yOffset, xSize, ySize, xWidth, constraints);
            Assert.AreEqual(9, constraints.Count(), "Did not create 9 row constraints");
            Keys<int> keysRow1 = constraints.First().Keys;
            string name = constraints.First().Name;
            Assert.AreEqual(9, keysRow1.Count, "First row did not have 9 keys");
            Assert.IsTrue(keysRow1.SetEquals(expected), "Set does not match");
            Assert.AreEqual("Row 1: At 265", name, "Row name does not match");
        }

        /// <summary>
        ///A test for AddColumns
        ///</summary>
        [TestMethod()]
        public void AddColumnsTest()
        {
            SudokuPuzzleBase target = CreateSudokuPuzzleBase();
            Keys<int> expected = new Keys<int>() {265, 286, 307, 328, 349, 370, 391, 412, 433};
            int xOffset = 12; 
            int yOffset = 12; 
            int xSize = 9; 
            int ySize = 9; 
            int xWidth = 21; 
            IConstraints<int> constraints = new Constraints<int>(); 
            target.AddColumns(xOffset, yOffset, xSize, ySize, xWidth, constraints);
            Assert.AreEqual(9, constraints.Count(), "Did not create 9 column constraints");
            Keys<int> keysColumn1 = constraints.First().Keys;
            string name = constraints.First().Name;
            Assert.AreEqual(9, keysColumn1.Count, "First column did not have 9 keys");
            Assert.IsTrue(keysColumn1.SetEquals(expected), "Set does not match");
            Assert.AreEqual("Column 1: At 265", name, "Column name does not match");
        }

        /// <summary>
        ///A test for AddGrids
        ///</summary>
        [TestMethod()]
        public void AddGridsTest()
        {
            SudokuPuzzleBase target = CreateSudokuPuzzleBase();
            Keys<int> expected = new Keys<int>() { 265, 266, 267, 286, 287, 288, 307, 308, 309 };
            int xOffset = 12;
            int yOffset = 12;
            int xSize = 9;
            int ySize = 9;
            int xWidth = 21;
            IConstraints<int> constraints = new Constraints<int>();
            target.AddGrids(xOffset, yOffset, xSize, ySize, xWidth, constraints);
            Assert.AreEqual(9, constraints.Count(), "Did not create 9 grid constraints");
            Keys<int> keysGrid1 = constraints.First().Keys;
            string name = constraints.First().Name;
            Assert.AreEqual(9, keysGrid1.Count, "First grid did not have 9 keys");
            Assert.IsTrue(keysGrid1.SetEquals(expected), "Set does not match");
        }

        internal virtual SudokuPuzzleBase CreateSudokuPuzzleBase()
        {
            // TODO: Instantiate an appropriate concrete class.
            SudokuPuzzleBase target = new SamuraiSudokuPuzzle();
            return target;
        }

        /// <summary>
        ///A test for AddGrid
        ///</summary>
        [TestMethod()]
        public void AddGridTest()
        {
            SudokuPuzzleBase target = CreateSudokuPuzzleBase(); 
            Keys<int> expected = new Keys<int>(){13,14,15,34,35,36,55,56,57};
            int xOffset = 12; 
            int yOffset = 0; 
            int xSize = 3; 
            int ySize = 3; 
            int xWidth = 21; 
            IConstraints<int> constraints = new Constraints<int>(); 
            target.AddGrid(xOffset, yOffset, xSize, ySize, xWidth, constraints);
            Assert.AreEqual(1, constraints.Count, "Constraint not created");
            Keys<int> keysOut = constraints.First().Keys;
            string name = constraints.First().Name;
            Assert.AreEqual(9, keysOut.Count, "Incorrect key count in grid");
            Assert.IsTrue(keysOut.SetEquals(expected), "Set does not match");
            Assert.AreEqual("Grid 5,1: At 13", name, "Column name does not match");
        }

        /// <summary>
        ///A test for AddGrid
        ///</summary>
        [TestMethod()]
        public void AddGridTest2()
        {
            SudokuPuzzleBase target = CreateSudokuPuzzleBase();
            Keys<int> expected = new Keys<int>() { 67, 68, 69, 88, 89, 90, 109, 110, 111 };
            int xOffset = 3;
            int yOffset = 3;
            int xSize = 3;
            int ySize = 3;
            int xWidth = 21;
            IConstraints<int> constraints = new Constraints<int>();
            target.AddGrid(xOffset, yOffset, xSize, ySize, xWidth, constraints);
            Assert.AreEqual(1, constraints.Count, "Constraint not created");
            Keys<int> keysOut = constraints.First().Keys;
            Assert.AreEqual(9, keysOut.Count, "Incorrect key count in grid");
            Assert.IsTrue(keysOut.SetEquals(expected), "Set does not match");
        }
    }
}
