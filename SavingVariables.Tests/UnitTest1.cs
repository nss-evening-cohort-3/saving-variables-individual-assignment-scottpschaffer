using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SavingVariables.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Regex_r1_outputs_valid()
        {
            Expression e1 = new Expression();
            string test1 = "q";
            string[] actResults = { "q", "error", "error" };
            string[] expResults = e1.Extract(test1);
            Assert.AreEqual(expResults[0], actResults[0]);
            //Assert.AreEqual(expResults[1], actResults[1]);
            //Assert.AreEqual(expResults[2], actResults[2]);
        }

        [TestMethod]
        public void Test_Regex_r2_output_valid_0()
        {
            Expression e2 = new Expression();
            string test2 = " r = 3";
            string[] actResults = { "r", "=", "3" };
            string[] expResults = e2.Extract(test2);
            Assert.AreEqual(expResults[0], actResults[0]);
        }

        [TestMethod]
        public void Test_Regex_r2_output_valid_1()
        {
            Expression e3 = new Expression();
            string test3 = " r = 3";
            string[] actResults = { "r", "=", "3" };
            string[] expResults = e3.Extract(test3);
            Assert.AreEqual(expResults[1], actResults[1]);
        }

        [TestMethod]
        public void Test_Regex_r2_output_valid_2()
        {
            Expression e4 = new Expression();
            string test4 = " r = 3";
            string[] actResults = { "r", "=", "3" };
            string[] expResults = e4.Extract(test4);
            Assert.AreEqual(expResults[2], actResults[2]);
        }
    }
}
