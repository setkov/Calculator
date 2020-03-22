using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTest
{
    [TestClass]
    public class ComputerTest
    {
        [TestMethod]
        public void TestCalcPriority()
        {
            string expression = "2.5+3*(1+2)-5";
            Computer computer = new Computer(expression);
            Assert.AreEqual((decimal)6.5, computer.Calc());
        }

        [TestMethod]
        public void TestCalcBrackets()
        {
            string expression = "(1+2)*(5-3)";
            Computer computer = new Computer(expression);
            Assert.AreEqual((decimal)6, computer.Calc());
        }
    }
}
