using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Syntax_Pars;

namespace Syntax_Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void SolveTest1()
        {
            Assert.AreEqual("2,097,144", Program.Solve(input: "7+2*2^2^(3-1*(-2.0)^3)/4-8*2+((1.00))", culture: new CultureInfo("en-US")));
            Assert.AreEqual("2 097 144", Program.Solve(input: "7+2*2^2^(3-1*(-2,0)^3)/4-8*2+((1,00))", culture: new CultureInfo("uk-UA")));
        }
        [TestMethod]
        public void SolveTest2()
        {
            Assert.AreEqual("-6,772", Program.Solve("-4^(((2^1/2)))-(22,5/5-1,2^3)", culture: new CultureInfo("uk-UA")));
            Assert.AreEqual("-6.772", Program.Solve("-4^(((2^1/2)))-(22.5/5-1.2^3)", culture: new CultureInfo("en-US")));
        }
        [TestMethod]
        public void SolveTest3()
        {
            for (decimal testInputNumber = 0.1M; testInputNumber < 1000M; testInputNumber += 0.1M)
            {
                Assert.AreEqual(testInputNumber.ToString(new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                                Program.Solve(testInputNumber.ToString(new CultureInfo("en-US")), culture: new CultureInfo("en-US")));
            }
        }
        [TestMethod]
        public void SolveTest4()
        {
            for (decimal testInputNumber = 0.1M; testInputNumber < 1000M; testInputNumber += 0.1M)
            {
                Assert.AreEqual(testInputNumber.ToString(new CultureInfo("uk-UA")).TrimEnd('0').TrimEnd(','),
                                Program.Solve(testInputNumber.ToString(new CultureInfo("uk-UA")), culture: new CultureInfo("uk-UA")));
            }
        }
        [TestMethod]
        public void SolveTest5()
        {
            Assert.AreEqual("4 294 967,295001", Program.Solve(input: "4294967,295001", culture: new CultureInfo("nb-NO")));
            Assert.AreEqual("42,94,967.295001", Program.Solve(input: "4294967.295001", culture: new CultureInfo("en-IN")));
            Assert.AreEqual("42,94,967.295001", Program.Solve(input: "4294967.2950010000000000", culture: new CultureInfo("hi-IN")));
        }
    }
}
