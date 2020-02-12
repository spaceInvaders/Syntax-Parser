using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void CheckInputTest()
        {
            string test = "0 + 1234   -  5,67/89*0";
            bool expected = true;
            bool actual = StringExtension.CheckInput(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckInputTest1()
        {
            string test = "ab0+1234-5,67/89*0";
            bool expected = false;
            bool actual = StringExtension.CheckInput(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest()
        {
            string test = "((2*4)-7)";
            int[] expected = new int[9] { 1, 2, 2, 2, 2, 1, 1, 1, 0 };
            int[] actual = StringExtension.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest1()
        {
            string test = "(2/2)+(3*3)";
            int[] expected = new int[11] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0 };
            int[] actual = StringExtension.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest2()
        {
            string test = "()";
            int[] expected = new int[2] { 1, 0 };
            int[] actual = StringExtension.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest()
        {
            string test = "(7+5)";
            string expected = "7+5";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest1()
        {
            string test = "((7+5))";
            string expected = "7+5";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest2()
        {
            string test = "(7+5)+(3-4)";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest4()
        {
            string test = "(7+5";
            string expected = "(7+5";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest5()
        {
            string test = "7+5)";
            string expected = "7+5)";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest3()
        {
            string test = "(((7+5)+(3-4)))";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SplitToNodesTest()
        {
            string test = "";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
    }
}
