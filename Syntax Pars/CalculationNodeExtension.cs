using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    internal static class CalculationNodeExtension
    {
        internal static decimal Calculate(this Node<CalculationElement> node)
        {
             return node.info.Operation switch
                {
                    Operation.Add => node.Left.Calculate() + node.Right.Calculate(),
                    Operation.Subtract => node.Left.Calculate() - node.Right.Calculate(),
                    Operation.Multiply => node.Left.Calculate() * node.Right.Calculate(),
                    Operation.Divide => node.Left.Calculate() / node.Right.Calculate(),
                    Operation.Number => Convert.ToDecimal(node.info.Number),
                    _ => throw new ArgumentException("Calculation failed"),
                };
        }
    }
}
