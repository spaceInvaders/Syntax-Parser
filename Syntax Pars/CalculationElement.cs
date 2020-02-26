using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    struct CalculationElement
    {
        internal Operation Operation { get; set; }
        internal decimal Number { get; set; }
    }
    enum Operation
    {
        Number,
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }
}

