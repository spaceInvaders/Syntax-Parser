using System;

namespace Syntax_Pars
{
    class ParsingException : Exception
    {
        public ParsingException(string message)
            : base(message)
        { }
    }

    class ParsingInvalidFragmentException : ParsingException
    {
        public ParsingInvalidFragmentException(string fragment, int firstEntry, int lastEntry)
            : base($"Invalid fragment '{fragment}' at indexes: {firstEntry}-{lastEntry}")
        {
            Fragment = fragment;
            FirstEntry = firstEntry;
            LastEntry = lastEntry;
        }
        public ParsingInvalidFragmentException(string fragment)
            : base($"Invalid fragment '{fragment}'")
        {
            Fragment = fragment;
        }
        public string Fragment { get; }
        public int FirstEntry { get; }
        public int LastEntry { get; }
    }

    class ParsingJustAnElementException : ParsingException
    {
        public ParsingJustAnElementException(string input)
            : base($"Just a '{input}'?")
        {
            Input = input;
        }
        public string Input { get; }
    }

    class ParsingMissedElementException : ParsingException
    {
        public ParsingMissedElementException(char element, int number)
            : base($"Missed {number} '{element}' ?")
        {
            Element = element;
            Number = number;
        }
        public int Number { get; }
        public char Element { get; }
    }

    class ParsingInvalidFirstElementException : ParsingException
    {
        public ParsingInvalidFirstElementException(char element)
            : base($"Invalid first element '{element}'")
        {
            Element = element;
        }
        public char Element { get; }
    }

    class ParsingInvalidLastElementException : ParsingException
    {
        public ParsingInvalidLastElementException(char element, int location)
            : base($"Invalid last element '{element}' at index {location}")
        {
            Element = element;
            Location = location;
        }
        public char Element { get; }
        public int Location { get; }
    }

    class ParsingInvalidElemenstException : ParsingException
    {
        public ParsingInvalidElemenstException(string invalidElements)
            : base($"Invalid elements '{invalidElements}'")
        {
            InvalidElements = invalidElements;
        }
        public string InvalidElements { get; }
    }
}
