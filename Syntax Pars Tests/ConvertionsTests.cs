using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorCore;
using System.Globalization;

namespace CalculatorCoreTests
{
    [TestClass]
    public class ConvertionsTests
    {
        [TestMethod]
        public void ConvertToBinaryTest()
        {
            Assert.AreEqual("110100010001.110001", Convertions.ConvertDecimalToBinaryString(input: 3345.7680098M, roundingPrecisionForBinary: 6, culture: new CultureInfo("ja-JP")));
            Assert.AreEqual("-10100100010100,11", Convertions.ConvertDecimalToBinaryString(input: -10516.7888M, roundingPrecisionForBinary: 4, culture: new CultureInfo("fr-FR")));
            Assert.AreEqual("10000000000000000000000000000000", Convertions.ConvertDecimalToBinaryString(input: 2147483648M, roundingPrecisionForBinary: 5, culture: new CultureInfo("uk-UA")));
            Assert.AreEqual("0.000110011", Convertions.ConvertDecimalToBinaryString(input: 0.1M, roundingPrecisionForBinary: 10, culture: new CultureInfo("en-US")));
        }
    }
}
