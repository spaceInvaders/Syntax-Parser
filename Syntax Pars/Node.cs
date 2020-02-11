using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    class Node<T>
    {
        public Node<T> left;
        public Node<T> right;
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
