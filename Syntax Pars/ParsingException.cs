using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    public class ParsingException : Exception
    {
        public ParsingException(string message)
            : base(message)
        { }
    }
}
