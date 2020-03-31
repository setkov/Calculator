using System;
using System.Globalization;

namespace Calculator
{
    public enum TokenType
    {
        Number,
        Operator,
        LeftBracket,
        RithtBracket,
        Unknown
    }

    public enum OperatorType
    {
        UnaryNegation,
        Division,
        Multiplication,
        Subtraction,
        Addition
    }

    public class Token
    {
        private static readonly NumberStyles NumberStyle = NumberStyles.Number;
        private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public TokenType TokenType { get; }
        public string Value { private set;  get; }
        public decimal? Number
        {
            get
            {
                if (TokenType == TokenType.Number && decimal.TryParse(Value, NumberStyle, NumberFormat, out decimal number))
                {
                    return number;
                }
                return null;
            }
        }
        public OperatorType? Operator { get; }
        public int? Priority
        {
            get
            {
                if (TokenType == TokenType.Operator)
                {
                    return Operator switch
                    {
                        OperatorType.UnaryNegation => 6,
                        OperatorType.Division => 4,
                        OperatorType.Multiplication => 4,
                        OperatorType.Subtraction => 2,
                        OperatorType.Addition => 2,
                        _ => null,
                    };
                }
                if (TokenType == TokenType.LeftBracket)
                {
                    return 0;
                }
                return null;
            }
        }

        public Token(char letter, bool isLead=false)
        {
            Value = letter.ToString();
            TokenType = SpotTokenType(letter);
            if (TokenType == TokenType.Operator)
            {
                if (letter == '-' && isLead)
                {
                    Operator = OperatorType.UnaryNegation;
                }
                else
                {
                    Operator = SpotOperator(letter);
                }
            }
        }

        private TokenType SpotTokenType(char letter)
        {
            if (char.IsDigit(letter) || letter == '.')
            {
                return TokenType.Number;
            }

            if ("/*-+".Contains(letter))
            {
                return TokenType.Operator;
            }

            if (letter == '(')
            {
                return TokenType.LeftBracket;
            }

            if (letter == ')')
            {
                return TokenType.RithtBracket;
            }

            return TokenType.Unknown;
        }

        private OperatorType? SpotOperator(char letter)
        {
            return letter switch
            {
                '/' => OperatorType.Division,
                '*' => OperatorType.Multiplication,
                '-' => OperatorType.Subtraction,
                '+' => OperatorType.Addition,
                _ => null,
            };
        }

        public void Append(char letter)
        {
            if (TokenType == SpotTokenType(letter))
            {
                Value += letter;
            }
        }
    }
}
