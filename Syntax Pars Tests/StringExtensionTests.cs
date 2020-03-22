using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;
using System.Globalization;
using FluentAssertions;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void SplitToNodesTest1()
        {
            string test = "2 + 3";
            Node<CalculationElement> expectedNode = new Node<CalculationElement>();
            expectedNode.info.Operation = Operation.Addition;
            expectedNode.Left = new Node<CalculationElement>();
            expectedNode.Right = new Node<CalculationElement>();
            expectedNode.Left.info.Operation = Operation.Number;
            expectedNode.Right.info.Operation = Operation.Number;
            expectedNode.Left.info.Number = 2M;
            expectedNode.Right.info.Number = 3M;
            Node<CalculationElement> actualNode = test.SplitToNodes(culture: new CultureInfo("zh-HK"));
            actualNode.Should().BeEquivalentTo(expectedNode);
        }
        [TestMethod]
        public void SplitToNodesTest2()
        {
            string test = "6-8/((  4.2))";
            Node<CalculationElement> expectedNode = new Node<CalculationElement>();
            expectedNode.info.Operation = Operation.Subtraction;
            expectedNode.Left = new Node<CalculationElement>();
            expectedNode.Right = new Node<CalculationElement>();
            expectedNode.Left.info.Operation = Operation.Number;
            expectedNode.Left.info.Number = 6M;
            expectedNode.Right.info.Operation = Operation.Division;
            expectedNode.Right.Left = new Node<CalculationElement>();
            expectedNode.Right.Right = new Node<CalculationElement>();
            expectedNode.Right.Left.info.Operation = Operation.Number;
            expectedNode.Right.Right.info.Operation = Operation.Number;
            expectedNode.Right.Left.info.Number = 8M;
            expectedNode.Right.Right.info.Number = 4.2M;
            Node<CalculationElement> actualNode = test.SplitToNodes(culture: new CultureInfo("ja-JP"));
            actualNode.Should().BeEquivalentTo(expectedNode);
        }
        [TestMethod]
        public void GrowNodeTreeTest1()
        {
            string test = "((2) + 3,0000)";
            Node<CalculationElement> expectedNode = new Node<CalculationElement>();
            expectedNode.info.Operation = Operation.Addition;
            expectedNode.Left = new Node<CalculationElement>();
            expectedNode.Right = new Node<CalculationElement>();
            expectedNode.Left.info.Operation = Operation.Number;
            expectedNode.Right.info.Operation = Operation.Number;
            expectedNode.Left.info.Number = 2M;
            expectedNode.Right.info.Number = 3M;
            Node<CalculationElement> actualNode = test.GrowNodeTree(culture: new CultureInfo("es-ES"));
            actualNode.Should().BeEquivalentTo(expectedNode);
        }
        [TestMethod]
        public void GrowNodeTreeTest2()
        {
            string test = "((386))";
            Node<CalculationElement> expectedNode = new Node<CalculationElement>();
            expectedNode.info.Operation = Operation.Number;
            expectedNode.info.Number = 386M;
            Node<CalculationElement> actualNode = test.GrowNodeTree(culture: new CultureInfo("es-ES"));
            actualNode.Should().BeEquivalentTo(expectedNode);
        }
        [TestMethod]
        public void CheckInputTest1()
        {
            try
            {
                StringExtension.CheckInput("ab0+1234-5.67/89hyt*0", culture: new CultureInfo("ja-JP"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: 'abhyt'");
            }
        }
        [TestMethod]
        public void CheckInputTest2()
        {
            Assert.AreEqual("50.123456789", StringExtension.CheckInput("50.123,456,789", culture: new CultureInfo("en-US")));
            Assert.AreEqual("50,123456789", StringExtension.CheckInput("50,123.456.789", culture: new CultureInfo("es-ES")));
        }
        [TestMethod]
        public void CheckInputTest3()
        {
            try
            {
                StringExtension.CheckInput("50,123.456", culture: new CultureInfo("uk-UA"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements: '.'");
            }
        }
        [TestMethod]
        public void CheckInputTest4()
        {
            try
            {
                StringExtension.CheckInput("50,123,,456,78", culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment ',,' at indexes: 6-7");
            }
        }
        [TestMethod]
        public void CheckInputTest5()
        {
            try
            {
                StringExtension.CheckInput("50..23,456,78", culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '..' at indexes: 2-3");
            }
        }
        [TestMethod]
        public void CheckInputTest6()
        {
            try
            {
                StringExtension.CheckInput("50.,23,456,78", culture: new CultureInfo("en-US"));
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid fragment '.,' at indexes: 2-3");
            }
        }
    }
}
