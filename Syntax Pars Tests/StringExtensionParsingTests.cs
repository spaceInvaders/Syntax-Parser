using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class StringExtensionParsingTests
    {
        [TestMethod]
        public void FindLastOerationWithPriorityPlusMinusTest1()
        {
            string testInput = "8-7^2+9*1";
            int actualLastOperationIndex = testInput.FindLastOerationWithPriorityPlusMinus();
            int expectedLastOperationIndex = 5;
            Assert.AreEqual(expectedLastOperationIndex, actualLastOperationIndex);
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
        public void ParseInputStringTest1()
        {
            try
            {
                "++9".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '++'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest2()
        {
            try
            {
                "9-*0".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '-*'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest3()
        {
            try
            {
                "+".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Just a '+'?");
            }
        }
        [TestMethod]
        public void ParseInputStringTest4()
        {
            try
            {
                "7+".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid last element '+'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest5()
        {
            try
            {
                ",03".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Separator at the beginning");
            }
        }
        [TestMethod]
        public void ParseInputStringTest6()
        {
            try
            {
                "8889,7087,03".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Double separator ',7087,'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest7()
        {
            try
            {
                "403,".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Separator at the end");
            }
        }

        [TestMethod]
        public void ParseInputStringTest8()
        {
            try
            {
                "(*6)".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '(*'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest9()
        {
            try
            {
                "()".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Empty brackets");
            }
        }
        [TestMethod]
        public void ParseInputStringTest10()
        {
            try
            {
                ",(2+4)".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Separator at the beginning");
            }
        }
        [TestMethod]
        public void ParseInputStringTest11()
        {
            try
            {
                ")(2+4)".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Missed 1 opening bracket(s)?");
            }
        }

        [TestMethod]
        public void ParseInputStringTest12()
        {
            try
            {
                "-((2)+4),".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment '),'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest13()
        {
            try
            {
                "(9+0)(2-4)".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid fragment ')('");
            }
        }
        [TestMethod]
        public void ParseInputStringTest14()
        {
            try
            {
                "((9+0)+(2-4)(".ParseInputString();
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Missed 2 closing bracket(s)?");
            }
        }
        [TestMethod]
        public void ParseInputStringTest15()
        {
            string test = "-(6)";
            string expected = "0-(6)";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParseInputStringTest16()
        {
            string test = "(-6)-(+7)+(-(-8))";
            string expected = "(0-6)-(0+7)+(0-(0-8))";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void ParseInputStringTest17()
        {
            try
            {
                StringExtension.ParseInputString("a&#######89+0b0");
            }
            catch (ParsingException exception)
            {
                StringAssert.Contains(exception.Message, "Invalid figures: a&#b");
            }
        }
        [TestMethod]
        public void ParseInputStringTest18()
        {
            string test = "2,9450000000";
            string expected = "2,945";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParseInputStringTest19()
        {
            string test = "2,9450000000+200+2,0000";
            string expected = "2,945+200+2";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParseInputStringTest20()
        {
            string test = "2,9+2,0";
            string expected = "2,9+2";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParseInputStringTest21()
        {
            string test = "2,00000*2";
            string expected = "2*2";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParseInputStringTest22()
        {
            string test = "200000*2";
            string expected = "200000*2";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParseInputStringTest23()
        {
            string test = "2,0000";
            string expected = "2";
            string actual = StringExtension.ParseInputString(test);
            Assert.AreEqual(expected, actual);
        }
    }
}

