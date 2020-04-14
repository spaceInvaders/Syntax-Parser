using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCore
{
    /// <summary>
    /// Contains all constants and readonly fields
    /// </summary>
    internal static class CalculationConstants
    {
        internal const char Zero = '0';
        internal const char Plus = '+';
        internal const char Minus = '-';
        internal const char Multiply = '*';
        internal const char Divide = '/';
        internal const char Power = '^';
        internal const char OpeningBracket = '(';
        internal const char ClosingBracket = ')';
        internal const char Comma = ',';
        internal const char Dot = '.';
        internal const char PiChar = 'p';

        internal static readonly string PlusMinMultDivPowBrackets =
            new string(new char[] { Plus, Minus, Multiply, Divide, Power, OpeningBracket, ClosingBracket });

        internal static readonly string PlusMinMultDivPow =
            new string(new char[] { Plus, Minus, Multiply, Divide, Power });


        internal static readonly string PlusMinMultDivPowClosBrack =
            new string(new char[] { Plus, Minus, Multiply, Divide, Power, ClosingBracket });

        internal static readonly string PlusMinMultDivPowOpenBrack =
            new string(new char[] { Plus, Minus, Multiply, Divide, Power, OpeningBracket });
    }
}
