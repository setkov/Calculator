using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTest
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void TestNumber()
        {
            Token token = new Token('5');
            Assert.AreEqual(TokenType.Number, token.TokenType);
            Assert.AreEqual((decimal)5, token.Number);
        }

        [TestMethod]
        public void TestNumbers()
        {
            Token token = new Token('5');
            token.Append('.');
            token.Append('5');
            Assert.AreEqual(TokenType.Number, token.TokenType);
            Assert.AreEqual((decimal)5.5, token.Number);
        }

        [TestMethod]
        public void TestOperatorUnarNeg()
        {
            Token token = new Token('-', true);
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual(OperatorType.UnaryNegation, token.Operator);
        }

        [TestMethod]
        public void TestOperatorDiv()
        {
            Token token = new Token('/');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual(OperatorType.Division, token.Operator);
        }

        [TestMethod]
        public void TestOperatorMult()
        {
            Token token = new Token('*');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual(OperatorType.Multiplication, token.Operator);
        }

        [TestMethod]
        public void TestOperatorSub()
        {
            Token token = new Token('-');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual(OperatorType.Subtraction, token.Operator);
        }

        [TestMethod]
        public void TestOperatorAdd()
        {
            Token token = new Token('+');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
            Assert.AreEqual(OperatorType.Addition, token.Operator);
        }

        [TestMethod]
        public void TestLeftBracket()
        {
            Token token = new Token('(');
            Assert.AreEqual(TokenType.LeftBracket, token.TokenType);
        }

        [TestMethod]
        public void TestRirhtBracket()
        {
            Token token = new Token(')');
            Assert.AreEqual(TokenType.RithtBracket, token.TokenType);
        }

        [TestMethod]
        public void TestUnknown()
        {
            Token token = new Token(',');
            Assert.AreEqual(TokenType.Unknown, token.TokenType);
        }


    }
}
