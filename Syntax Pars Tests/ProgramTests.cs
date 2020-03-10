using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void SolveTest1()
        {
            Assert.AreEqual("2097144", Program.Solve("7+2*2^2^(3-1*(-2.0)^3)/4-8*2+((1,00))"));
            Assert.AreEqual("-6,772", Program.Solve("-4^(((2^1/2)))-(22,5/5-1,2^3)"));
        }
    }
}
