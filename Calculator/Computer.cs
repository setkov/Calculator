using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Computer
    {
        public string Expression { get; }
        public Parser Parser { get; }

        public Computer(string expression)
        {
            Expression = expression;
            Parser = new Parser(expression);
        }

        public decimal Calc()
        {
            var stack = new Stack<decimal>();
            foreach (Token token in Parser.Postfix)
            {
                switch (token.TokenType)
                {
                    case TokenType.Number:
                        stack.Push((decimal)token.Number);
                        break;

                    case TokenType.Operator:
                        if (token.Operator == OperatorType.UnaryNegation)
                        {
                            var operand = stack.Pop();
                            stack.Push(-operand);
                        }
                        else
                        {
                            var rightOperand = stack.Pop();
                            var leftOperand = stack.Pop();
                            switch (token.Operator)
                            {
                                case OperatorType.Division:
                                    stack.Push(leftOperand / rightOperand);
                                    break;
                                case OperatorType.Multiplication:
                                    stack.Push(leftOperand * rightOperand);
                                    break;
                                case OperatorType.Subtraction:
                                    stack.Push(leftOperand - rightOperand);
                                    break;
                                case OperatorType.Addition:
                                    stack.Push(leftOperand + rightOperand);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            return stack.Pop();
        }
    }
}
