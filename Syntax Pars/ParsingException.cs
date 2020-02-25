using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    class ParsingException : Exception
    {
        public ParsingException(string message)
            : base(message)
        { }
    }
}
