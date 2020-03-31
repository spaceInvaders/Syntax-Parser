using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorCore;
using System.Globalization;

namespace CalculatorCoreTests
{
    [TestClass]
    public class CalculationDecimalCheckerTests
    {
        [TestMethod]
        public void CheckInputTest1()
        {
            try
            {
                CalculationDecimalChecker.CheckInput("ab0+1234-5.67/89hyt*0", culture: new CultureInfo("ja-JP"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: 'abhyt'");
            }
        }
        [TestMethod]
        public void CheckInputTest2()
        {
            Assert.AreEqual("50.123456789", CalculationDecimalChecker.CheckInput("50.123,456,789", culture: new CultureInfo("en-US")));
            Assert.AreEqual("50,123456789", CalculationDecimalChecker.CheckInput("50,123.456.789", culture: new CultureInfo("es-ES")));
        }
        [TestMethod]
        public void CheckInputTest3()
        {
            try
            {
                CalculationDecimalChecker.CheckInput("50,123.456", culture: new CultureInfo("uk-UA"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: '.'");
            }
        }
        [TestMethod]
        public void CheckInputTest4()
        {
            try
            {
                CalculationDecimalChecker.CheckInput("50,123,,456,78", culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment ',,' at indexes: 6-7");
            }
        }
        [TestMethod]
        public void CheckInputTest5()
        {
            try
            {
                CalculationDecimalChecker.CheckInput("50..23,456,78", culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '..' at indexes: 2-3");
            }
        }
        [TestMethod]
        public void CheckInputTest6()
        {
            try
            {
                CalculationDecimalChecker.CheckInput("50.,23,456,78", culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '.,' at indexes: 2-3");
            }
        }
        [TestMethod]
        public void ParseInputTest1()
        {
            try
            {
                CalculationDecimalChecker.CheckInput(input: "++9", culture: new CultureInfo("zh-HK"));
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
                CalculationDecimalChecker.CheckInput(input: "9-*0", culture: new CultureInfo("es-ES"));
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
                CalculationDecimalChecker.CheckInput(input: "+", culture: new CultureInfo("hr-HR"));
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
                CalculationDecimalChecker.CheckInput(input: "7+", culture: new CultureInfo("ja-JP"));
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
                CalculationDecimalChecker.CheckInput(input: ".03", culture: new CultureInfo("hr-HR"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid first element '.'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest6()
        {
            try
            {
                CalculationDecimalChecker.CheckInput(input: "8889.7087.03", culture: new CultureInfo("ja-JP"));
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
                CalculationDecimalChecker.CheckInput(input: "403.", culture: new CultureInfo("en-US"));
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
                CalculationDecimalChecker.CheckInput(input: "(*6)", culture: new CultureInfo("en-US"));
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
                CalculationDecimalChecker.CheckInput(input: "()", culture: new CultureInfo("en-US"));
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
                CalculationDecimalChecker.CheckInput(input: ".(2+4)", culture: new CultureInfo("uk-UA"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: '.'");
            }
        }
        [TestMethod]
        public void ParseInputStringTest11()
        {
            try
            {
                CalculationDecimalChecker.CheckInput(input: ")(2+4)", culture: new CultureInfo("en-US"));
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
                CalculationDecimalChecker.CheckInput(input: "-((2)+4),", culture: new CultureInfo("ru-RU"));
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
                CalculationDecimalChecker.CheckInput(input: "(9+0)(2-4)", culture: new CultureInfo("ru-RU"));
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
                CalculationDecimalChecker.CheckInput(input: "((9+0)+(2-4)(", culture: new CultureInfo("ru-RU"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Missed 2 ')' ?");
            }
        }
        [TestMethod]
        public void ParseInputStringTest15()
        {
            Assert.AreEqual("0-(6)", CalculationDecimalChecker.CheckInput("-(6)", culture: new CultureInfo("ru-RU")));
            Assert.AreEqual("(0-6)-(0+7)+(0-(0-8))", CalculationDecimalChecker.CheckInput("(-6)-(+7)+(-(-8))", culture: new CultureInfo("ru-RU")));
        }
        [TestMethod]
        public void ParseInputStringTest16()
        {
            try
            {
                CalculationDecimalChecker.CheckInput("a&#######89+0b0", culture: new CultureInfo("ru-RU"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: 'a&#b'");
            }
        }
    }
}

