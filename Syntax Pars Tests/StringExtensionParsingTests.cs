using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class StringExtensionParsingTests
    {
        [TestMethod]
        public void CheckOnOperationsTest1()
        {
            try
            {
                "++9".CheckOnOperations();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '++'");
            }
        }
        [TestMethod]
        public void CheckOnOperationsTest2()
        {
            try
            {
                "9-*0".CheckOnOperations();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '-*'");
            }
        }
        [TestMethod]
        public void CheckOnOperationsTest3()
        {
            try
            {
                "+".CheckOnOperations();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Just a '+'?");
            }
        }
        [TestMethod]
        public void CheckOnOperationsTest4()
        {
            try
            {
                "7+".CheckOnOperations();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid last element '+'");
            }
        }
        [TestMethod]
        public void CheckOnCommaTest1()
        {
            try
            {
                ",03".CheckOnComma();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Comma at the beginning");
            }
        }
        [TestMethod]
        public void CheckOnCommaTest2()
        {
            try
            {
                "8889,7087,03".CheckOnComma();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Double comma ',7087,'");
            }
        }
        [TestMethod]
        public void CheckOnCommaTest3()
        {
            try
            {
                "403,".CheckOnComma();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Comma at the end");
            }
        }

        [TestMethod]
        public void CheckOnBracketsTest2()
        {
            try
            {
                "(*6)".CheckOnBrackets();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '(*'");
            }
        }
        [TestMethod]
        public void CheckOnBracketsTest3()
        {
            try
            {
                "()".CheckOnBrackets();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Empty brackets");
            }
        }
        [TestMethod]
        public void CheckOnBracketsTest4()
        {
            try
            {
                ",(2+4)".CheckOnBrackets();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment ',('");
            }
        }
        [TestMethod]
        public void CheckOnBracketsTest5()
        {
            try
            {
                ")(2+4)".CheckOnBrackets();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Missed 1 opening bracket(s)?");
            }
        }

        [TestMethod]
        public void CheckOnBracketsTest8()
        {
            try
            {
                "-((2)+4),".CheckOnBrackets();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '),'");
            }
        }
        [TestMethod]
        public void CheckOnBracketsTest9()
        {
            try
            {
                "(9+0)(2-4)".CheckOnBrackets();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment ')('");
            }
        }
        [TestMethod]
        public void CheckOnBracketsTest10()
        {
            try
            {
                "((9+0)+(2-4)(".CheckOnBrackets();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Missed 2 closing bracket(s)?");
            }
        }
        [TestMethod]
        public void ValidatedUnaryMinusStringTest1()
        {
            string test = "-(6)";
            string expected = "0-(6)";
            string actual = StringExtension.ValidatedUnaryMinusString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ValidatedUnaryMinusStringTest2()
        {
            string test = "(-6)-(+7)";
            string expected = "(0-6)-(0+7)";
            string actual = StringExtension.ValidatedUnaryMinusString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest1()
        {
            string test = "((2*4)-7)";
            int[] expected = new int[9] { 1, 2, 2, 2, 2, 1, 1, 1, 0 };
            int[] actual = StringExtension.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest2()
        {
            string test = "(2/2)+(3*3)";
            int[] expected = new int[11] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0 };
            int[] actual = StringExtension.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest3()
        {
            string test = "()";
            int[] expected = new int[2] { 1, 0 };
            int[] actual = StringExtension.BracketsLevel(test);
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BracketsLevelTest4()
        {
            try
            {
                StringExtension.BracketsLevel("(1))))))");
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Missed 5 opening bracket(s)?");
            }
        }
        [TestMethod]
        public void TrimBracketsStringTest1()
        {
            string test = "(7+5)";
            string expected = "7+5";
            string actual = StringExtension.TrimBracketsString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsStringTest2()
        {
            string test = "((7+5))";
            string expected = "7+5";
            string actual = StringExtension.TrimBracketsString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsStringTest3()
        {
            string test = "(7+5)+(3-4)";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtension.TrimBracketsString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsStringTest4()
        {
            string test = "(7+5";
            string expected = "(7+5";
            string actual = StringExtension.TrimBracketsString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest6()
        {
            string test = "7+5)";
            string expected = "7+5)";
            string actual = StringExtension.TrimBracketsString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest7()
        {
            string test = "(((7+5)+(3-4)))";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtension.TrimBracketsString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckOnFiguresTest1()
        {
            try
            {
                StringExtension.CheckOnFigures("a&#######89+0b0");
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid figures: a&#b");
            }
        }
        [TestMethod]
        public void TrimExcessiveZerosStringTest1()
        {
            string test = "2,9450000000";
            string expected = "2,945";
            string actual = StringExtension.TrimExcessiveZerosString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimExcessiveZerosStringTest2()
        {
            string test = "2,9450000000*2";
            string expected = "2,945*2";
            string actual = StringExtension.TrimExcessiveZerosString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimExcessiveZerosStringTest3()
        {
            string test = "2,00000*2";
            string expected = "2*2";
            string actual = StringExtension.TrimExcessiveZerosString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimExcessiveZerosStringTest4()
        {
            string test = "200000*2";
            string expected = "200000*2";
            string actual = StringExtension.TrimExcessiveZerosString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimExcessiveZerosStringTest5()
        {
            string test = "2,0000";
            string expected = "2";
            string actual = StringExtension.TrimExcessiveZerosString(test);
            Assert.AreEqual(expected, actual);
        }
    }
}

