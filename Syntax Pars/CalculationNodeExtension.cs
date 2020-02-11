using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    public static class CalculationNodeExtension
    {
        internal static decimal Calculate(this Node<CalculationNode> node)
        {
            return node.info.Operation switch
            {
                Operation.Add => node.left.Calculate() + node.right.Calculate(),
                Operation.Subtract => node.left.Calculate() - node.right.Calculate(),
                Operation.Multiply => node.left.Calculate() * node.right.Calculate(),
                Operation.Divide => node.left.Calculate() / node.right.Calculate(),
                Operation.Number => Convert.ToDecimal(node.info.Number),
                _ => throw new Exception("Calculation failed"),
            };
        }
    }
}
