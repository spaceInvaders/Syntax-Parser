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
    }
}
