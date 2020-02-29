using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    static class CalculationNodeExtension
    {
        internal static decimal Calculate(this Node<CalculationElement> node)
        {
             return node.info.Operation switch
                {
                    Operation.Addition => node.Left.Calculate() + node.Right.Calculate(),
                    Operation.Subtraction => node.Left.Calculate() - node.Right.Calculate(),
                    Operation.Multiplication => node.Left.Calculate() * node.Right.Calculate(),
                    Operation.Division => node.Left.Calculate() / node.Right.Calculate(),
                    Operation.Number => Convert.ToDecimal(node.info.Number),
                    _ => throw new ArgumentException("Calculation failed"),
                };
        }
    }
}
