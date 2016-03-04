﻿namespace Rubberduck.Parsing.Preprocessing
{
    public sealed class LogicalEqualsExpression : Expression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;
        private readonly VBAOptionCompare _optionCompare;

        public LogicalEqualsExpression(IExpression left, IExpression right, VBAOptionCompare optionCompare)
        {
            _left = left;
            _right = right;
            _optionCompare = optionCompare;
        }

        public override IValue Evaluate()
        {
            var left = _left.Evaluate();
            var right = _right.Evaluate();
            if (left == null || right == null)
            {
                return null;
            }
            if (
                (left.ValueType == ValueType.String || left.ValueType == ValueType.Empty)
                && (right.ValueType == ValueType.String || right.ValueType == ValueType.Empty))
            {
                var leftValue = left.AsString;
                var rightValue = right.AsString;
                if (_optionCompare == VBAOptionCompare.Binary)
                {
                    return new BoolValue(string.CompareOrdinal(leftValue, rightValue) == 0);
                }
                else
                {
                    return new BoolValue(leftValue.CompareTo(rightValue) == 0);
                }
            }
            else
            {
                var leftValue = left.AsDecimal;
                var rightValue = right.AsDecimal;
                return new BoolValue(leftValue == rightValue);
            }
        }
    }
}