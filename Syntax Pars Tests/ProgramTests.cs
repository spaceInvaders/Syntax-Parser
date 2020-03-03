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
            string test = "7+2*2^2^(3-1*(-2)^3)/4-8*2+((1))";
            string expected = "2097144";
            string actual = Program.Solve(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SolveTest2()
        {
            string test = "-4^(((2^1/2)))-(22,5/5-1,2^3)";
            string expected = "-6,772";
            string actual = Program.Solve(test);
            Assert.AreEqual(expected, actual);
        }
    }
}
