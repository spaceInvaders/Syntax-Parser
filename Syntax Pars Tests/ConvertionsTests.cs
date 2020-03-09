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
            decimal expected = 110100010001.110001M;
            decimal actual = Convertions.ConvertToBinary(test, 6.0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ConvertToBinaryTest2()
        {
            decimal test = -10516.7888M;
            decimal expected = -10100100010100.11001M;
            decimal actual = Convertions.ConvertToBinary(test, 5.0);
            Assert.AreEqual(expected, actual);
        }
    }
}
