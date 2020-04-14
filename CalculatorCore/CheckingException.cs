using System;

namespace CalculatorCore
{
    public class CheckingException : Exception
    {
        internal CheckingException(string message)
            : base(message)
        { }
    }

    public class CheckingInvalidFragmentException : CheckingException
    {
        internal CheckingInvalidFragmentException(string fragment, int firstEntry, int lastEntry)
            : base($"Invalid fragment '{fragment}' at indexes: {firstEntry}-{lastEntry}")
        {
            Fragment = fragment;
            FirstEntry = firstEntry;
            LastEntry = lastEntry;
        }
        internal CheckingInvalidFragmentException(string fragment)
            : base($"Invalid fragment '{fragment}'")
        {
            Fragment = fragment;
        }
        internal string Fragment { get; private set; }
        internal int FirstEntry { get; private set; }
        internal int LastEntry { get; private set; }
    }

    public class CheckingJustAnElementException : CheckingException
    {
        internal CheckingJustAnElementException(string input)
            : base($"Just a '{input}'?")
        {
            Input = input;
        }
        internal string Input { get; private set; }
    }

    public class CheckingMissedElementException : CheckingException
    {
        internal CheckingMissedElementException(char element, int number)
            : base($"Missed {number} '{element}' ?")
        {
            Element = element;
            Number = number;
        }
        internal int Number { get; private set; }
        internal char Element { get; private set; }
    }

    public class CheckingInvalidFirstElementException : CheckingException
    {
        internal CheckingInvalidFirstElementException(char element)
            : base($"Invalid first element '{element}'")
        {
            Element = element;
        }
        internal char Element { get; private set; }
    }

    public class CheckingInvalidLastElementException : CheckingException
    {
        internal CheckingInvalidLastElementException(char element, int location)
            : base($"Invalid last element '{element}' at index {location}")
        {
            Element = element;
            Location = location;
        }
        internal char Element { get; private set; }
        internal int Location { get; private set; }
    }

    public class CheckingInvalidElemenstException : CheckingException
    {
        internal CheckingInvalidElemenstException(string invalidElements)
            : base($"Invalid elements: '{invalidElements}'")
        {
            InvalidElements = invalidElements;
        }
        internal string InvalidElements { get; private set; }
    }
}
