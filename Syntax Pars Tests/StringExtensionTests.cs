using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syntax_Pars;
using System;

namespace Syntax_Pars_Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void CheckOnOperationsTest1()
        {
            // Arrange, Act
            try
            {
                "++9".CheckOnOperations();
            }
            catch (ParsingException exception)
            {
                // Assert
                StringAssert.Contains(exception.Message, "Invalid fragment '++'");
            }
        }
        //[TestMethod]
        //public void CheckOnOperationsTest2()
        //{
        //    try
        //    {
        //        "9-*0".CheckOnOperations();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Invalid fragment '-*'");
        //    }
        //}
        //[TestMethod]
        //public void CheckOnOperationsTest3()
        //{
        //    try
        //    {
        //        "+".CheckOnOperations();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Just a '+'?");
        //    }
        //}
        //[TestMethod]
        //public void CheckOnOperationsTest4()
        //{
        //    try
        //    {
        //        "7+".CheckOnOperations();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Invalid last element '+'");
        //    }
        //}
        //[TestMethod]
        //public void CheckOnCommaTest1()
        //{
        //    try
        //    {
        //        ",03".CheckOnComma();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Comma at the beginning");
        //    }
        //}
        //[TestMethod]
        //public void CheckOnCommaTest2()
        //{
        //    try
        //    {
        //        "8889,7087,03".CheckOnComma();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Double comma ',7087,'");
        //    }
        //}
        //[TestMethod]
        //public void CheckOnCommaTest3()
        //{
        //    try
        //    {
        //        "403,".CheckOnComma();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Comma at the end");
        //    }
        //}
        
        //[TestMethod]
        //public void CheckOnBracketsTest2()
        //{
        //    try
        //    {
        //        "(*6)".CheckOnBrackets();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Empty brackets");
        //    }
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest3()
        //{
        //    try
        //    {
        //        "()".CheckOnBrackets();
        //    }
        //    catch (ParsingException exception)
        //    {
        //        StringAssert.Contains(exception.Message, "Empty brackets");
        //    }
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest4()
        //{
        //    string test = ",(2+4)";
        //    string expected = null;
        //    string actual = StringExtension.CheckOnBrackets(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest5()
        //{
        //    string test = ")(2+4)";
        //    string expected = null;
        //    string actual = StringExtension.CheckOnBrackets(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest6()
        //{
        //    string test = "((2)+4)";
        //    string expected = "((2)+4)";
        //    string actual = StringExtension.CheckOnBrackets(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest7()
        //{
        //    string test = "-((2)+4)";
        //    string expected = "-((2)+4)";
        //    string actual = StringExtension.CheckOnBrackets(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest8()
        //{
        //    string test = "-((2)+4),";
        //    string expected = null;
        //    string actual = StringExtension.CheckOnBrackets(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest9()
        //{
        //    string test = "(9+0)(2-4)";
        //    string expected = null;
        //    string actual = StringExtension.CheckOnBrackets(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnBracketsTest10()
        //{
        //    string test = "(9+0)+(2-4)(";
        //    string expected = null;
        //    string actual = StringExtension.CheckOnBrackets(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void ValidatedUnaryMinusStringTest1()
        //{
        //    string test = "-(6)";
        //    string expected = "0-(6)";
        //    string actual = StringExtension.ValidatedUnaryMinusString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void ValidatedUnaryMinusStringTest2()
        //{
        //    string test = "(-6)-(+7)";
        //    string expected = "(0-6)-(0+7)";
        //    string actual = StringExtension.ValidatedUnaryMinusString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckInputTest1()
        //{
        //    string test = "0 + 1.234   -  5.67/89*0";
        //    string expected = "0+1,234-5,67/89*0";
        //    string actual = StringExtension.CheckInput(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckInputTest2()
        //{
        //    string test = "ab0+1234-5,67/89*0";
        //    string expected = null;
        //    string actual = StringExtension.CheckInput(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckInputTest3()
        //{
        //    string test = "    ";
        //    string expected = null;
        //    string actual = StringExtension.CheckInput(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void BracketsLevelTest1()
        //{
        //    string test = "((2*4)-7)";
        //    int[] expected = new int[9] { 1, 2, 2, 2, 2, 1, 1, 1, 0 };
        //    int[] actual = StringExtension.BracketsLevel(test);
        //    CollectionAssert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void BracketsLevelTest2()
        //{
        //    string test = "(2/2)+(3*3)";
        //    int[] expected = new int[11] { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0 };
        //    int[] actual = StringExtension.BracketsLevel(test);
        //    CollectionAssert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void BracketsLevelTest3()
        //{
        //    string test = "()";
        //    int[] expected = new int[2] { 1, 0 };
        //    int[] actual = StringExtension.BracketsLevel(test);
        //    CollectionAssert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void TrimBracketsStringTest1()
        //{
        //    string test = "(7+5)";
        //    string expected = "7+5";
        //    string actual = StringExtension.TrimBracketsString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void TrimBracketsStringTest2()
        //{
        //    string test = "((7+5))";
        //    string expected = "7+5";
        //    string actual = StringExtension.TrimBracketsString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void TrimBracketsStringTest3()
        //{
        //    string test = "(7+5)+(3-4)";
        //    string expected = "(7+5)+(3-4)";
        //    string actual = StringExtension.TrimBracketsString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void TrimBracketsStringTest4()
        //{
        //    string test = "(7+5";
        //    string expected = "(7+5";
        //    string actual = StringExtension.TrimBracketsString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void TrimBracketsTest6()
        //{
        //    string test = "7+5)";
        //    string expected = "7+5)";
        //    string actual = StringExtension.TrimBracketsString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void TrimBracketsTest7()
        //{
        //    string test = "(((7+5)+(3-4)))";
        //    string expected = "(7+5)+(3-4)";
        //    string actual = StringExtension.TrimBracketsString(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnFiguresTest1()
        //{
        //    string test = "sgs";
        //    string expected = null;
        //    string actual = StringExtension.CheckOnFigures(test);
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod]
        //public void CheckOnFiguresTest2()
        //{
        //    string test = "(90-0)";
        //    string expected = "(90-0)";
        //    string actual = StringExtension.CheckOnFigures(test);
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
