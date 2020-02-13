using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    class Node<T>
    {
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public CalculationNode info;
    }

    enum Operation
    {
        Number,
        Add,
        Subtract,
        Multiply,
        Divide,
    }

    struct CalculationNode
    {
        public Operation Operation { get; set; }
        public decimal Number { get; set; }
    }
}
