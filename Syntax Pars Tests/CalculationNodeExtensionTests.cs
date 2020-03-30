using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class CalculationNodeExtensionTests
    {
        [TestMethod]
        public void SplitToNodesTest1()
        {
            //string test = "6-8/((4))";
            
            ICalculationOperation node_6 = new Number(6M);
            ICalculationOperation node_8 = new Number(8M);
            ICalculationOperation node_4 = new Number(4M);
            ICalculationOperation node_8_divide_4 = new Division(node_8, node_4);
            ICalculationOperation node_6_minus_8_divide_4 = new Subtraction(node_6, node_8_divide_4);

            decimal actualResult = node_6_minus_8_divide_4.Calculate();
            decimal expectedResult = 4M;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
