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
            Node<CalculationElement> testNode = new Node<CalculationElement>();
            testNode.info.Operation = Operation.Subtraction;
            testNode.Left = new Node<CalculationElement>();
            testNode.Right = new Node<CalculationElement>();
            testNode.Left.info.Operation = Operation.Number;
            testNode.Left.info.Number = 6M;
            testNode.Right.info.Operation = Operation.Division;
            testNode.Right.Left = new Node<CalculationElement>();
            testNode.Right.Right = new Node<CalculationElement>();
            testNode.Right.Left.info.Operation = Operation.Number;
            testNode.Right.Right.info.Operation = Operation.Number;
            testNode.Right.Left.info.Number = 8M;
            testNode.Right.Right.info.Number = 4M;
            decimal actualResult = testNode.Calculate();
            decimal expectedResult = 4M;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
