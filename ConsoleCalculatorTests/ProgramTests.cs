using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using ConsoleCalculator;

namespace CalculatorCoreTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ExecuteExpressionTest1()
        {
            Assert.AreEqual("2,097,144", Program.ExecuteExpression(input: "7+2*2^2^(3-1*(-2.0)^3)/4-8*2+((1.00))", culture: new CultureInfo("en-US")));
            Assert.AreEqual("2 097 144", Program.ExecuteExpression(input: "7+2*2^2^(3-1*(-2,0)^3)/4-8*2+((1,00))", culture: new CultureInfo("uk-UA")));
        }
        [TestMethod]
        public void ExecuteExpressionTest2()
        {
            Assert.AreEqual("-6,772", Program.ExecuteExpression("-4^(((2^1/2)))-(22,5/5-1,2^3)", culture: new CultureInfo("uk-UA")));
            Assert.AreEqual("-6.772", Program.ExecuteExpression("-4^(((2^1/2)))-(22.5/5-1.2^3)", culture: new CultureInfo("en-US")));
        }
        [TestMethod]
        public void ExecuteExpressionTest3()
        {
            for (decimal testInputNumber = 0.1M; testInputNumber < 1000M; testInputNumber += 0.1M)
            {
                Assert.AreEqual(testInputNumber.ToString(new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                                Program.ExecuteExpression(testInputNumber.ToString(new CultureInfo("en-US")), culture: new CultureInfo("en-US")));
            }
        }
        [TestMethod]
        public void ExecuteExpressionTest4()
        {
            for (decimal testInputNumber = 0.1M; testInputNumber < 1000M; testInputNumber += 0.1M)
            {
                Assert.AreEqual(testInputNumber.ToString(new CultureInfo("uk-UA")).TrimEnd('0').TrimEnd(','),
                                Program.ExecuteExpression(testInputNumber.ToString(new CultureInfo("uk-UA")), culture: new CultureInfo("uk-UA")));
            }
        }
        [TestMethod]
        public void ExecuteExpressionTest5()
        {
            Assert.AreEqual("4 294 967,295001", Program.ExecuteExpression(input: "4294967,295001", culture: new CultureInfo("nb-NO")));
            Assert.AreEqual("42,94,967.295001", Program.ExecuteExpression(input: "4294967.295001", culture: new CultureInfo("en-IN")));
            Assert.AreEqual("42,94,967.295001", Program.ExecuteExpression(input: "4294967.2950010000000000", culture: new CultureInfo("hi-IN")));
        }
    }
}
