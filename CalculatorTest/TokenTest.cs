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
        public void TestOperatorDiv()
        {
            Token token = new Token('/');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
        }

        [TestMethod]
        public void TestOperatorMult()
        {
            Token token = new Token('*');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
        }

        [TestMethod]
        public void TestOperatorMinus()
        {
            Token token = new Token('-');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
        }

        [TestMethod]
        public void TestOperatorPlus()
        {
            Token token = new Token('+');
            Assert.AreEqual(TokenType.Operator, token.TokenType);
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
