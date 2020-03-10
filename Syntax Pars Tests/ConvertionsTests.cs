using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Tests
{
    [TestClass]
    public class ConvertionsTests
    {
        [TestMethod]
        public void ConvertToBinaryTest()
        {
            Assert.AreEqual("110100010001,110001", Convertions.ConvertDecimalToBinaryString(input: 3345.7680098M, roundingPrecision: 6));
            Assert.AreEqual("-10100100010100,11", Convertions.ConvertDecimalToBinaryString(input: -10516.7888M, roundingPrecision: 4));
            Assert.AreEqual("10000000000000000000000000000000", Convertions.ConvertDecimalToBinaryString(input: 2147483648M, roundingPrecision: 5));
        }
    }
}
