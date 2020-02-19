enum Operation
{
    Number,
    Add,
    Subtract,
    Multiply,
    Divide,
}

struct CalculationElement
{
    public Operation Operation { get; set; }
    public decimal Number { get; set; }
}