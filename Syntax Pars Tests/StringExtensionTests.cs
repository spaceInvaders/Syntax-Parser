﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;
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
            Node<CalculationElement> actualNode = test.SplitToNodes();
            actualNode.Should().BeEquivalentTo(expectedNode);
        }
        [TestMethod]
        public void SplitToNodesTest2()
        {
            string test = "6-8/((4,2))";
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
            Node<CalculationElement> actualNode = test.SplitToNodes();
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
            Node<CalculationElement> actualNode = test.GrowNodeTree();
            actualNode.Should().BeEquivalentTo(expectedNode);
        }
        [TestMethod]
        public void GrowNodeTreeTest2()
        {
            string test = "((386))";
            Node<CalculationElement> expectedNode = new Node<CalculationElement>();
            expectedNode.info.Operation = Operation.Number;
            expectedNode.info.Number = 386M;
            Node<CalculationElement> actualNode = test.GrowNodeTree();
            actualNode.Should().BeEquivalentTo(expectedNode);
        }
        [TestMethod]
        public void CheckInputTest1()
        {
            string test = "0 + 1.234   -  5.67/89*0";
            string expected = "0+1,234-5,67/89*0";
            string actual = StringExtension.CheckInput(test);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckInputTest2()
        {
            try
            {
                StringExtension.CheckInput("ab0+1234-5,67/89hyt*0");
            }
            catch (ParsingException exception)
            {
                Assert.AreEqual(exception.Message, "Invalid elements 'abhyt'");
            }
        }
        [TestMethod]
        public void CheckInputTest3()
        {
            string test = " (  ( (-2.660000000000     00000/2)))";
            string expected = "0-2,66/2";
            string actual = StringExtension.CheckInput(test);
            Assert.AreEqual(expected, actual);
        }
    }
}
