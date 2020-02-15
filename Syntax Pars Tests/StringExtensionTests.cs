using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void CheckOnOperationsTest1()
        {
            string test = "++9";
            string expected = null;
            string actual = StringExtensionBase.CheckOnOperations(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnOperationsTest2()
        {
            string test = "9-*0";
            string expected = null;
            string actual = StringExtensionBase.CheckOnOperations(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnCommaTest1()
        {
            string test = "9,7";
            string expected = "9,7";
            string actual = StringExtensionBase.CheckOnComma(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnCommaTest2()
        {
            string test = "9,7087,0";
            string expected = null;
            string actual = StringExtensionBase.CheckOnComma(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnCommaTest3()
        {
            string test = "9,70+87,0";
            string expected = "9,70+87,0";
            string actual = StringExtensionBase.CheckOnComma(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnCommaTest4()
        {
            string test = "(9,7087,)";
            string expected = null;
            string actual = StringExtensionBase.CheckOnComma(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest2()
        {
            string test = "(*6)";
            string expected = null;
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest3()
        {
            string test = "()";
            string expected = null;
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest4()
        {
            string test = ",(2+4)";
            string expected = null;
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest5()
        {
            string test = ")(2+4)";
            string expected = null;
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest6()
        {
            string test = "((2)+4)";
            string expected = "((2)+4)";
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest7()
        {
            string test = "-((2)+4)";
            string expected = "-((2)+4)";
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest8()
        {
            string test = "-((2)+4),";
            string expected = null;
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest9()
        {
            string test = "(9+0)(2-4)";
            string expected = null;
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnBracketsTest10()
        {
            string test = "(9+0)+(2-4)(";
            string expected = null;
            string actual = StringExtensionBase.CheckOnBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnMinusTest1()
        {
            string test = "-(6)";
            string expected = "0-(6)";
            string actual = StringExtensionBase.CheckOnMinus(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnMinusTest2()
        {
            string test = "(-6)-(+7)";
            string expected = "(0-6)-(0+7)";
            string actual = StringExtensionBase.CheckOnMinus(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckInputTest1()
        {
            string test = "0 + 1.234   -  5.67/89*0";
            string expected = "0+1,234-5,67/89*0";
            string actual = StringExtensionBase.CheckInput(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckInputTest2()
        {
            string test = "ab0+1234-5,67/89*0";
            string expected = null;
            string actual = StringExtensionBase.CheckInput(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest1()
        {
            string test = "((2*4)-7)";
            int[] expected = new int[9] { 1, 2, 2, 2, 2, 1, 1, 1, 0 };
            int[] actual = StringExtensionBase.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest2()
        {
            string test = "(2/2)+(3*3)";
            int[] expected = new int[11] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0 };
            int[] actual = StringExtensionBase.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest3()
        {
            string test = "()";
            int[] expected = new int[2] { 1, 0 };
            int[] actual = StringExtensionBase.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest1()
        {
            string test = "(7+5)";
            string expected = "7+5";
            string actual = StringExtensionBase.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest2()
        {
            string test = "((7+5))";
            string expected = "7+5";
            string actual = StringExtensionBase.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest3()
        {
            string test = "(7+5)+(3-4)";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtensionBase.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest4()
        {
            string test = "(7+5";
            string expected = "(7+5";
            string actual = StringExtensionBase.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest6()
        {
            string test = "7+5)";
            string expected = "7+5)";
            string actual = StringExtensionBase.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest7()
        {
            string test = "(((7+5)+(3-4)))";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtensionBase.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnFiguresTest1()
        {
            string test = "sgs";
            string expected = null;
            string actual = StringExtensionBase.CheckOnFigures(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnFiguresTest2()
        {
            string test = "(90-0)";
            string expected = "(90-0)";
            string actual = StringExtensionBase.CheckOnFigures(test);
            Assert.AreEqual(expected, actual);
        }
    }
}
