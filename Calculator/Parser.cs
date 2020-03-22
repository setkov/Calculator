using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Parser
    {
        public string Expression { get; }
        public List<Token> Infix { get; }
        public string Error { get; }
        public List<Token> Postfix { get; }

        public Parser(string expression)
        {
            Expression = expression;
            Infix = ParseToInfix(expression);
            Error = TestInfix(Infix);
            if (string.IsNullOrEmpty(Error))
            {
                Postfix = ProcessingToPostfix(Infix);
            }
        }

        private List<Token> ParseToInfix(string expression)
        {
            var tokens = new List<Token>();
            if (expression.Length > 0)
            {
                var token = new Token(expression[0]);
                for (int i = 1; i < expression.Length; i++)
                {
                    var nextToken = new Token(expression[i]);
                    if (token.TokenType == TokenType.Operator || token.TokenType == TokenType.LeftBracket || token.TokenType == TokenType.RithtBracket || token.TokenType != nextToken.TokenType)
                    {
                        tokens.Add(token);
                        token = nextToken;
                    }
                    else
                    {
                        token.Append(expression[i]);
                    }
                }
                tokens.Add(token);
            }
            return tokens;
        }

        private string TestInfix(List<Token> tokens)
        {
            int numBracket = 0;
            Token tokenPrev = null;

            foreach (Token token in tokens)
            {
                if (token.TokenType == TokenType.Unknown)
                {
                    return "Error: unknown token " + token.Value;
                }

                if (token.TokenType == TokenType.Operator && tokenPrev.TokenType == TokenType.Operator)
                {
                    return "Error: wrong operator " + tokenPrev.Value + token.Value;
                }

                if (token.TokenType == TokenType.Number && !token.Number.HasValue)
                {
                    return "Error: wrong number " + token.Value;
                }

                if (token.TokenType == TokenType.LeftBracket)
                {
                    numBracket++;
                }

                if (token.TokenType == TokenType.RithtBracket && --numBracket < 0)
                {
                    return "Error: wrong right bracket";
                }

                if (token.TokenType == TokenType.LeftBracket && tokenPrev != null && tokenPrev.TokenType != TokenType.Operator && tokenPrev.TokenType != TokenType.LeftBracket)
                {
                    return "Error: wrong near left bracket";
                }

                if (token.TokenType == TokenType.Number && tokenPrev != null && tokenPrev.TokenType == TokenType.RithtBracket)
                {
                    return "Error: wrong near right bracket";
                }

                tokenPrev = token;
            }

            if (numBracket > 0)
            {
                return "Error: wrong left bracket";
            }

            return string.Empty;
        }

        private List<Token> ProcessingToPostfix(List<Token> infix)
        {
            var sortStation = new Stack<Token>();
            var postfix = new List<Token>();
            foreach (Token token in infix)
            {
                switch (token.TokenType)
                {
                    case TokenType.Number:
                        postfix.Add(token);
                        break;

                    case TokenType.Operator:
                        while (sortStation.Count > 0 && sortStation.Peek().Priority >= token.Priority)
                        {
                            postfix.Add(sortStation.Pop());
                        }
                        sortStation.Push(token);
                        break;

                    case TokenType.LeftBracket:
                        sortStation.Push(token);
                        break;

                    case TokenType.RithtBracket:
                        while (sortStation.Count > 0 && sortStation.Peek().TokenType != TokenType.LeftBracket)
                        {
                            postfix.Add(sortStation.Pop());
                        }
                        if (sortStation.Peek().TokenType == TokenType.LeftBracket)
                        {
                            var bracket = sortStation.Pop();
                        }
                        break;

                    default:
                        break;
                }
            }
            while (sortStation.Count > 0 )
            {
                if (sortStation.Peek().TokenType == TokenType.Operator)
                {
                    postfix.Add(sortStation.Pop());
                }
            }
            return postfix;
        }

    }
}
