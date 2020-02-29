using System;

namespace Syntax_Pars
{
    class ParsingException : Exception
    {
        public ParsingException(string message)
            : base(message)
        { }
    }
}
