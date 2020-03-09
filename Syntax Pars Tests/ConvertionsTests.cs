using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Tests
{
    [TestClass]
    public class ConvertionsTests
    {
        [TestMethod]
        public void ConvertToBinaryTest1()
        {
            decimal test = 3345.7680098M;
            string expected = "110100010001,110001";
            string actual = Convertions.ConvertDecimalToBinaryString(input: test, roundingPrecision: 6);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ConvertToBinaryTest2()
        {
            decimal test = -10516.7888M;
            string expected = "-10100100010100,11";
            string actual = Convertions.ConvertDecimalToBinaryString(input: test, roundingPrecision: 4);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ConvertToBinaryTest3()
        {
            decimal test = 2147483648M;
            string expected = "10000000000000000000000000000000";
            string actual = Convertions.ConvertDecimalToBinaryString(input: test, roundingPrecision: 5);
            Assert.AreEqual(expected, actual);
        }
    }
}
