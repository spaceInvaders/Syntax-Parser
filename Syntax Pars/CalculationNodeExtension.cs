using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    public static class CalculationNodeExtension
    {
        public static bool CalculationIsAllowed { get; private set; } = true;

        internal static decimal Calculate(this Node<CalculationNode> node)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CalculationIsAllowed = false;
                return 0;
            }
        }
    }
}
