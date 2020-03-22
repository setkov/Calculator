using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void TestUnknownToken()
        {
            string expression = "1_2";
            Parser parser = new Parser(expression);
            StringAssert.StartsWith(parser.Error, "Error: unknown token");
        }

        [TestMethod]
        public void TestWrongOperator()
        {
            string expression = "1//2";
            Parser parser = new Parser(expression);
            StringAssert.StartsWith(parser.Error, "Error: wrong operator");
        }

        [TestMethod]
        public void TestWrongNumber()
        {
            string expression = "1..2//2";
            Parser parser = new Parser(expression);
            StringAssert.StartsWith(parser.Error, "Error: wrong number");
        }

        [TestMethod]
        public void TestWrongBbracketLeft()
        {
            string expression = "((1+2)*3";
            Parser parser = new Parser(expression);
            StringAssert.StartsWith(parser.Error, "Error: wrong left bracket");
        }

        [TestMethod]
        public void TestWrongBbracketRight()
        {
            string expression = "(1+2))*3";
            Parser parser = new Parser(expression);
            StringAssert.StartsWith(parser.Error, "Error: wrong right bracket");
        }

        [TestMethod]
        public void TestWrongNearLeftBbracket()
        {
            string expression = "8(1+2)";
            Parser parser = new Parser(expression);
            StringAssert.StartsWith(parser.Error, "Error: wrong near left bracket");
        }

        [TestMethod]
        public void TestWrongNearRightBbracket()
        {
            string expression = "(1+2)8";
            Parser parser = new Parser(expression);
            StringAssert.StartsWith(parser.Error, "Error: wrong near right bracket");
        }

        [TestMethod]
        public void TestInfix()
        {
            string expression = "2.5+3*(1+2)-5";
            List<Token> infix = new List<Token>()
            {
                new Token("2.5"),
                new Token('+'),
                new Token('3'),
                new Token('*'),
                new Token('('),
                new Token('1'),
                new Token('+'),
                new Token('2'),
                new Token(')'),
                new Token('-'),
                new Token('5')
            };
            Parser parser = new Parser(expression);
            CollectionAssert.AreEqual(infix.Select(t => t.Value).ToList(), parser.Infix.Select(t => t.Value).ToList());
        }

        [TestMethod]
        public void TestPostfix()
        {
            string expression = "2.5+3*(1+2)-5";
            List<Token> postfix = new List<Token>()
            {
                new Token("2.5"),
                new Token('3'),
                new Token('1'),
                new Token('2'),
                new Token('+'),
                new Token('*'),
                new Token('+'),
                new Token('5'),
                new Token('-')
            };
            Parser parser = new Parser(expression);
            CollectionAssert.AreEqual(postfix.Select(t => t.Value).ToList(), parser.Postfix.Select(t => t.Value).ToList());
        }
    }
}
