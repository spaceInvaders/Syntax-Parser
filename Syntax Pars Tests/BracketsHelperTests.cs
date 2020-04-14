using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorCore;

namespace CalculatorCoreTests
{
    class BracketsHelperTests
    {
        #region BracketsLevelTests

        [TestMethod]
        public void BracketsLevelTest1()
        {
            string test = "((2*4)-7)";
            int[] expected = new int[9] { 1, 2, 2, 2, 2, 1, 1, 1, 0 };
            int[] actual = BracketsHelper.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest2()
        {
            string test = "(2/2)+(3*3)";
            int[] expected = new int[11] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0 };
            int[] actual = BracketsHelper.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest3()
        {
            string test = "()";
            int[] expected = new int[2] { 1, 0 };
            int[] actual = BracketsHelper.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest4()
        {
            try
            {
                BracketsHelper.BracketsLevel("(1))))))");
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Missed 5 '(' ?");
            }
        }

        #endregion

        #region TrimBracketsTests

        [TestMethod]
        public void TrimBracketsTest1()
        {
            Assert.AreEqual("7+5", BracketsHelper.TrimBrackets("(7+5)"));
            Assert.AreEqual("7+5", BracketsHelper.TrimBrackets("(((7+5)))"));
            Assert.AreEqual("(7+5)+(3-4)", BracketsHelper.TrimBrackets("(((7+5)+(3-4)))"));
        }
        [TestMethod]
        public void TrimBracketsTest2()
        {
            Assert.AreEqual("(7+5", BracketsHelper.TrimBrackets("(7+5"));
            Assert.AreEqual("7+5)", BracketsHelper.TrimBrackets("7+5)"));
            Assert.AreEqual("(7+5)+(3-4)", BracketsHelper.TrimBrackets("(7+5)+(3-4)"));
        }

        #endregion

    }
}
