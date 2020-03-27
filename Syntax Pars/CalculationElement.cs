namespace Syntax_Pars
{
    struct CalculationElement
    {
        internal Operation Operation { get; set; }
        internal decimal Number { get; set; }

        internal CalculationElement(Operation operation)
        {
            Operation = operation;
            Number = 0;
        }

        internal CalculationElement(decimal number)
        {
            Operation = Operation.Number;
            Number = number;
        }
    }

    enum Operation
    {
        Number,
        Addition,
        Subtraction,
        Multiplication,
        Division,
        ToThePower
    }
}


