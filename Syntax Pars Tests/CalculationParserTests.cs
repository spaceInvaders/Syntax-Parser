using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorCore;
using System.Globalization;

namespace CalculatorCoreTests
{
    [TestClass]
    public class CalculationParserTests
    {
        [TestMethod]
        public void GrowNodeTreeTest1()
        {
            string test = "2 + 3";

            ICalculationOperation node_2 = new Number(2M);
            ICalculationOperation node_3 = new Number(3M);
            ICalculationOperation node_2_plus_3 = new Addition(node_2, node_3);

            ICalculationOperation actualNode = CalculationParser.GrowNodeTree(input: test, culture: new CultureInfo("zh-HK"));

            Assert.AreEqual(actualNode, node_2_plus_3);
        }
        [TestMethod]
        public void GrowNodeTreeTest2()
        {
            string test = "6-8/((  4.2))";

            ICalculationOperation node_6 = new Number(6M);
            ICalculationOperation node_8 = new Number(8M);
            ICalculationOperation node_4_point_2 = new Number(4.2M);
            ICalculationOperation node_8_divide_4_point_2 = new Division(node_8, node_4_point_2);
            ICalculationOperation node_6_minus_8_divide_4_point_2 = new Subtraction(node_6, node_8_divide_4_point_2);

            ICalculationOperation actualNode = CalculationParser.GrowNodeTree(input: test, culture: new CultureInfo("ja-JP"));

            Assert.AreEqual(actualNode, node_6_minus_8_divide_4_point_2);
        }
        [TestMethod]
        public void GrowNodeTreeTest3()
        {
            string test = "((2) + 3,0000)";

            ICalculationOperation node_2 = new Number(2M);
            ICalculationOperation node_3 = new Number(3.0000M);
            ICalculationOperation node_2_plus_3 = new Addition(node_2, node_3);

            ICalculationOperation actualNode = CalculationParser.GrowNodeTree(input: test, culture: new CultureInfo("es-ES"));

            Assert.AreEqual(actualNode, node_2_plus_3);
        }
        [TestMethod]
        public void GrowNodeTreeTest4()
        {
            string test = "((386))";

            ICalculationOperation node_386 = new Number(386M);

            ICalculationOperation actualNode = CalculationParser.GrowNodeTree(input: test, culture: new CultureInfo("es-ES"));

            Assert.AreEqual(actualNode, node_386);
        }
    }
}
