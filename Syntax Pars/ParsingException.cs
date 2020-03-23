using System;

namespace Syntax_Pars
{
    class ParsingException : Exception
    {
        internal ParsingException(string message)
            : base(message)
        { }
    }

    class ParsingInvalidFragmentException : ParsingException
    {
        internal ParsingInvalidFragmentException(string fragment, int firstEntry, int lastEntry)
            : base($"Invalid fragment '{fragment}' at indexes: {firstEntry}-{lastEntry}")
        {
            Fragment = fragment;
            FirstEntry = firstEntry;
            LastEntry = lastEntry;
        }
        internal ParsingInvalidFragmentException(string fragment)
            : base($"Invalid fragment '{fragment}'")
        {
            Fragment = fragment;
        }
        internal string Fragment { get; }
        internal int FirstEntry { get; }
        internal int LastEntry { get; }
    }

    class ParsingJustAnElementException : ParsingException
    {
        internal ParsingJustAnElementException(string input)
            : base($"Just a '{input}'?")
        {
            Input = input;
        }
        internal string Input { get; }
    }

    class ParsingMissedElementException : ParsingException
    {
        internal ParsingMissedElementException(char element, int number)
            : base($"Missed {number} '{element}' ?")
        {
            Element = element;
            Number = number;
        }
        internal int Number { get; }
        internal char Element { get; }
    }

    class ParsingInvalidFirstElementException : ParsingException
    {
        internal ParsingInvalidFirstElementException(char element)
            : base($"Invalid first element '{element}'")
        {
            Element = element;
        }
        internal char Element { get; }
    }

    class ParsingInvalidLastElementException : ParsingException
    {
        internal ParsingInvalidLastElementException(char element, int location)
            : base($"Invalid last element '{element}' at index {location}")
        {
            Element = element;
            Location = location;
        }
        internal char Element { get; }
        internal int Location { get; }
    }

    class ParsingInvalidElemenstException : ParsingException
    {
        internal ParsingInvalidElemenstException(string invalidElements)
            : base($"Invalid elements: '{invalidElements}'")
        {
            InvalidElements = invalidElements;
        }
        internal string InvalidElements { get; }
    }
}
