using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;
using System.Globalization;

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
                Assert.AreEqual(exception.Message, "Missed 5 '(' ?");
            }
        }
        [TestMethod]
        public void TrimBracketsStringTest1()
        {
            Assert.AreEqual("7+5", StringExtension.TrimBracketsString("(7+5)"));
            Assert.AreEqual("7+5", StringExtension.TrimBracketsString("(((7+5)))"));
            Assert.AreEqual("(7+5)+(3-4)", StringExtension.TrimBracketsString("(((7+5)+(3-4)))"));
        }
        [TestMethod]
        public void TrimBracketsStringTest2()
        {
            Assert.AreEqual("(7+5", StringExtension.TrimBracketsString("(7+5"));
            Assert.AreEqual("7+5)", StringExtension.TrimBracketsString("7+5)"));
            Assert.AreEqual("(7+5)+(3-4)", StringExtension.TrimBracketsString("(7+5)+(3-4)"));
        }
        [TestMethod]
        public void ParseInputStringTest1()
        {
            try
            {
                "++9".ParseInputString(culture: new CultureInfo("zh-HK"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '++' at indexes: 0-1");
            }
        }
        [TestMethod]
        public void ParseInputStringTest2()
        {
            try
            {
                "9-*0".ParseInputString(culture: new CultureInfo("es-ES"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '-*' at indexes: 1-2");
            }
        }
        [TestMethod]
        public void ParseInputStringTest3()
        {
            try
            {
                "+".ParseInputString(culture: new CultureInfo("hr-HR"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Just a '+'?");
            }
        }
        [TestMethod]
        public void ParseInputStringTest4()
        {
            try
            {
                "7+".ParseInputString(culture: new CultureInfo("ja-JP"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid last element '+' at index 1");
            }
        }
        [TestMethod]
        public void ParseInputStringTest5()
        {
            try
            {
                ".03".ParseInputString(culture: new CultureInfo("hr-HR"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements '.'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest6()
        {
            try
            {
                "8889.7087.03".ParseInputString(culture: new CultureInfo("ja-JP"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '.7087.' at indexes: 4-9");
            }
        }
        [TestMethod]
        public void ParseInputStringTest7()
        {
            try
            {
                "403.".ParseInputString(culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid last element '.' at index 3");
            }
        }
        [TestMethod]
        public void ParseInputStringTest8()
        {
            try
            {
                "(*6)".ParseInputString(culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '(*' at indexes: 0-1");
            }
        }
        [TestMethod]
        public void ParseInputStringTest9()
        {
            try
            {
                "()".ParseInputString(culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '()'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest10()
        {
            try
            {
                ".(2+4)".ParseInputString(culture: new CultureInfo("uk-UA"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements '.'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest11()
        {
            try
            {
                ")(2+4)".ParseInputString(culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Missed 1 '(' ?");
            }
        }
        [TestMethod]
        public void ParseInputStringTest12()
        {
            try
            {
                "-((2)+4),".ParseInputString(culture: new CultureInfo("ru-RU"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '),' at indexes: 7-8");
            }
        }
        [TestMethod]
        public void ParseInputStringTest13()
        {
            try
            {
                "(9+0)(2-4)".ParseInputString(culture: new CultureInfo("ru-RU"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment ')(' at indexes: 4-5");
            }
        }
        [TestMethod]
        public void ParseInputStringTest14()
        {
            try
            {
                "((9+0)+(2-4)(".ParseInputString(culture: new CultureInfo("ru-RU"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Missed 2 ')' ?");
            }
        }
        [TestMethod]
        public void ParseInputStringTest15()
        {
            Assert.AreEqual("0-(6)", StringExtension.ParseInputString("-(6)", culture: new CultureInfo("ru-RU")));
            Assert.AreEqual("(0-6)-(0+7)+(0-(0-8))", StringExtension.ParseInputString("(-6)-(+7)+(-(-8))", culture: new CultureInfo("ru-RU")));
        }
        [TestMethod]
        public void ParseInputStringTest16()
        {
            try
            {
                StringExtension.ParseInputString("a&#######89+0b0", culture: new CultureInfo("ru-RU"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements 'a&#b'");
            }
        }
    }
}

