using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void TrimBracketsTest()
        {
            string test = "(7+5)";
            string expected = "7+5";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBracketsTest1()
        {
            string test = "((7+5))";
            string expected = "7+5";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBrackets2()
        {
            string test = "(7+5)+(3-4)";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBrackets4()
        {
            string test = "(7+5";
            string expected = "(7+5";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBrackets5()
        {
            string test = "7+5)";
            string expected = "7+5)";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TrimBrackets3()
        {
            string test = "((7+5)+(3-4))";
            string expected = "(7+5)+(3-4)";
            string actual = StringExtension.TrimBrackets(test);
            Assert.AreEqual(expected, actual);
        }
    }
}
