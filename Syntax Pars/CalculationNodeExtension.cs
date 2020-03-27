using System;

namespace Syntax_Pars
{
    static class CalculationNodeExtension
    {
        internal static decimal Calculate(this Node<CalculationElement> node)
        {
            return node.Info.Operation switch
            {
                Operation.Addition => node.Left.Calculate() + node.Right.Calculate(),
                Operation.Subtraction => node.Left.Calculate() - node.Right.Calculate(),
                Operation.Multiplication => node.Left.Calculate() * node.Right.Calculate(),
                Operation.Division => node.Left.Calculate() / node.Right.Calculate(),
                Operation.ToThePower => (decimal)Math.Pow((double)node.Left.Calculate(), (double)node.Right.Calculate()),
                Operation.Number => Convert.ToDecimal(node.Info.Number),
                _ => throw new ParsingException("Calculation failed"),
            };
        }
    }
}
