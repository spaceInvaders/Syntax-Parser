using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Syntax_Pars.Test
{
    [TestClass]
    public class StringExtensionTest
    {
        [TestMethod]
        public void TrimBrackets_StringInBrackets_StringWithOutBrackets()
        {
            string test = "((7 + 5))";
            string expected = "7 + 5";

            string actual = StringExtension.TrimBrackets(test);

            Assert.AreEqual(expected, actual);
        }
    }
}
