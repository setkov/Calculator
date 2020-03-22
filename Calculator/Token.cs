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

    public class Token
    {
        private static readonly NumberStyles NumberStyle = NumberStyles.Number;
        private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public string Value { private set;  get; }
        public TokenType TokenType { get; }
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
        public int? Priority
        {
            get
            {
                if (TokenType == TokenType.Operator || TokenType == TokenType.LeftBracket)
                {
                    return Value switch
                    {
                        "/" => 4,
                        "*" => 4,
                        "-" => 2,
                        "+" => 2,
                        "(" => 0,
                        _ => -1,
                    };
                }
                return null;
            }
        }

        public Token(char letter)
        {
            Value = letter.ToString();
            TokenType = SpotTokenType(letter);
        }
        public Token(string word)
        {
            Value = word;
            TokenType = SpotTokenType(word[0]);
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

        public void Append(char letter)
        {
            if (TokenType == SpotTokenType(letter))
            {
                Value += letter;
            }
        }
    }
}
