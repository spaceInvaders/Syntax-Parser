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
            Node<CalculationElement> testNode = new Node<CalculationElement>
            {
                Info = new CalculationElement(operation: Operation.Subtraction),
                Left = new Node<CalculationElement>(),
                Right = new Node<CalculationElement>()
            };
            testNode.Left.Info = new CalculationElement(number: 6M);
            testNode.Right.Info = new CalculationElement(operation: Operation.Division);
            testNode.Right.Left = new Node<CalculationElement>();
            testNode.Right.Right = new Node<CalculationElement>();
            testNode.Right.Left.Info = new CalculationElement(number: 8M);
            testNode.Right.Right.Info = new CalculationElement(number: 4M);
            decimal actualResult = testNode.Calculate();
            decimal expectedResult = 4M;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
