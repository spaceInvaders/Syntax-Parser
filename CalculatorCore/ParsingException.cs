using System;

namespace CalculatorCore
{
    public class ParsingException : Exception
    {
        internal ParsingException(string message)
            : base(message)
        { }
    }

    public class ParsingInvalidFragmentException : ParsingException
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
        internal string Fragment { get; private set; }
        internal int FirstEntry { get; private set; }
        internal int LastEntry { get; private set; }
    }

    public class ParsingJustAnElementException : ParsingException
    {
        internal ParsingJustAnElementException(string input)
            : base($"Just a '{input}'?")
        {
            Input = input;
        }
        internal string Input { get; private set; }
    }

    public class ParsingMissedElementException : ParsingException
    {
        internal ParsingMissedElementException(char element, int number)
            : base($"Missed {number} '{element}' ?")
        {
            Element = element;
            Number = number;
        }
        internal int Number { get; private set; }
        internal char Element { get; private set; }
    }

    public class ParsingInvalidFirstElementException : ParsingException
    {
        internal ParsingInvalidFirstElementException(char element)
            : base($"Invalid first element '{element}'")
        {
            Element = element;
        }
        internal char Element { get; private set; }
    }

    public class ParsingInvalidLastElementException : ParsingException
    {
        internal ParsingInvalidLastElementException(char element, int location)
            : base($"Invalid last element '{element}' at index {location}")
        {
            Element = element;
            Location = location;
        }
        internal char Element { get; private set; }
        internal int Location { get; private set; }
    }

    public class ParsingInvalidElemenstException : ParsingException
    {
        internal ParsingInvalidElemenstException(string invalidElements)
            : base($"Invalid elements: '{invalidElements}'")
        {
            InvalidElements = invalidElements;
        }
        internal string InvalidElements { get; private set; }
    }
}
