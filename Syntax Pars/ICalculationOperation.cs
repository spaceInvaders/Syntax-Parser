using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax_Pars
{
    internal interface ICalculationOperation
    {
        public decimal Calculate();
    }

    internal abstract class CalculationOperation
    {
        protected CalculationOperation(ICalculationOperation left, ICalculationOperation right)
        {
            if (left == null || right == null)
                throw new ParsingException(message: "Calculation failed");

            Left = left;
            Right = right;
        }

        internal ICalculationOperation Left { get; private set; }
        internal ICalculationOperation Right { get; private set; }

        override public bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            if (!(obj.GetType() == this.GetType())) { return false; }
            var calculationOperationObj = (CalculationOperation)obj;
            return (calculationOperationObj.Left.Equals(Left)) && (calculationOperationObj.Right.Equals(Right));
        }
    }

    internal class ToThePower : CalculationOperation, ICalculationOperation
    {
        internal ToThePower(ICalculationOperation left, ICalculationOperation right) : base(left, right) { }

        public decimal Calculate()
        {
            return (decimal)Math.Pow((double)Left.Calculate(), (double)Right.Calculate());
        }
    }

    internal class Division : CalculationOperation, ICalculationOperation
    {
        internal Division(ICalculationOperation left, ICalculationOperation right) : base(left, right) { }

        public decimal Calculate()
        {
            return Left.Calculate() / Right.Calculate();
        }
    }

    internal class Multiplication : CalculationOperation, ICalculationOperation
    {
        internal Multiplication(ICalculationOperation left, ICalculationOperation right) : base(left, right) { }

        public decimal Calculate()
        {
            return Left.Calculate() * Right.Calculate();
        }
    }

    internal class Addition : CalculationOperation, ICalculationOperation
    {
        internal Addition(ICalculationOperation left, ICalculationOperation right) : base(left, right) { }

        public decimal Calculate()
        {
            return Left.Calculate() + Right.Calculate();
        }
    }

    internal class Subtraction : CalculationOperation, ICalculationOperation
    {
        internal Subtraction(ICalculationOperation left, ICalculationOperation right) : base(left, right) { }

        public decimal Calculate()
        {
            return Left.Calculate() - Right.Calculate();
        }
    }

    internal class Number: ICalculationOperation
    {
        internal Number(decimal value)
        {
            Value = value;
        }

        internal decimal Value { get; private set; }

        public decimal Calculate()
        {
            return Value;
        }

        override public bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            if (!(obj is Number)) { return false; }
            return ((Number)obj).Value == Value;
        }
    }
}