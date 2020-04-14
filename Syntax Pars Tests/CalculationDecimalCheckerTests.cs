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
                CalculationDecimalChecker.VerifyInput("ab0+1234-5.67/89hyt*0", culture: new CultureInfo("ja-JP"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: 'abhyt'");
            }
        }
        [TestMethod]
        public void CheckInputTest2()
        {
            Assert.AreEqual("50.123456789", CalculationDecimalChecker.VerifyInput("50.123,456,789", culture: new CultureInfo("en-US")));
            Assert.AreEqual("50,123456789", CalculationDecimalChecker.VerifyInput("50,123.456.789", culture: new CultureInfo("es-ES")));
        }
        [TestMethod]
        public void CheckInputTest3()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput("50,123.456", culture: new CultureInfo("uk-UA"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: '.'");
            }
        }
        [TestMethod]
        public void CheckInputTest4()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput("50,123,,456,78", culture: new CultureInfo("en-US"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment ',,' at indexes: 6-7");
            }
        }
        [TestMethod]
        public void CheckInputTest5()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput("50..23,456,78", culture: new CultureInfo("en-US"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '..' at indexes: 2-3");
            }
        }
        [TestMethod]
        public void CheckInputTest6()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput("50.,23,456,78", culture: new CultureInfo("en-US"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '.,' at indexes: 2-3");
            }
        }
        [TestMethod]
        public void CheckInputTest7()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "++9", culture: new CultureInfo("zh-HK"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '++' at indexes: 0-1");
            }
        }
        [TestMethod]
        public void CheckInputTest8()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "9-*0", culture: new CultureInfo("es-ES"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '-*' at indexes: 1-2");
            }
        }
        [TestMethod]
        public void CheckInputTest9()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "+", culture: new CultureInfo("hr-HR"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Just a '+'?");
            }
        }
        [TestMethod]
        public void CheckInputTest10()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "7+", culture: new CultureInfo("ja-JP"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid last element '+' at index 1");
            }
        }
        [TestMethod]
        public void CheckInputTest11()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: ".03", culture: new CultureInfo("hr-HR"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid first element '.'");
            }
        }
        [TestMethod]
        public void CheckInputTest12()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "8889.7087.03", culture: new CultureInfo("ja-JP"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '.7087.' at indexes: 4-9");
            }
        }
        [TestMethod]
        public void CheckInputTest13()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "403.", culture: new CultureInfo("en-US"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid last element '.' at index 3");
            }
        }
        [TestMethod]
        public void CheckInputTest14()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "(*6)", culture: new CultureInfo("en-US"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '(*' at indexes: 0-1");
            }
        }
        [TestMethod]
        public void CheckInputTest15()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "()", culture: new CultureInfo("en-US"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '()'");
            }
        }
        [TestMethod]
        public void CheckInputTest16()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: ".(2+4)", culture: new CultureInfo("uk-UA"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: '.'");
            }
        }
        [TestMethod]
        public void CheckInputTest17()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: ")(2+4)", culture: new CultureInfo("en-US"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Missed 1 '(' ?");
            }
        }
        [TestMethod]
        public void CheckInputTest18()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "-((2)+4),", culture: new CultureInfo("ru-RU"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '),' at indexes: 7-8");
            }
        }
        [TestMethod]
        public void CheckInputTest19()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "(9+0)(2-4)", culture: new CultureInfo("ru-RU"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment ')(' at indexes: 4-5");
            }
        }
        [TestMethod]
        public void CheckInputTest20()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput(input: "((9+0)+(2-4)(", culture: new CultureInfo("ru-RU"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Missed 2 ')' ?");
            }
        }
        [TestMethod]
        public void CheckInputTest21()
        {
            Assert.AreEqual("0-(6)", CalculationDecimalChecker.VerifyInput("-(6)", culture: new CultureInfo("ru-RU")));
            Assert.AreEqual("(0-6)-(0+7)+(0-(0-8))", CalculationDecimalChecker.VerifyInput("(-6)-(+7)+(-(-8))", culture: new CultureInfo("ru-RU")));
        }
        [TestMethod]
        public void CheckInputTest22()
        {
            try
            {
                CalculationDecimalChecker.VerifyInput("a&#######89+0b0", culture: new CultureInfo("ru-RU"));
            }
            catch (CheckingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: 'a&#b'");
            }
        }
    }
}

