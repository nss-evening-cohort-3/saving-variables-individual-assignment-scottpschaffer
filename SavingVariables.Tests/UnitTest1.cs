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
            string[] actResults = { "q", "error" };
            string[] expResults = e1.Extract(test1);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_Regex_r2_output_valid_1()
        {
            Expression e3 = new Expression();
            string test3 = " r = 3";
            string[] actResults = { "r", "3" };
            string[] expResults = e3.Extract(test3);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_Regex_r2_output_valid_2()
        {
            Expression e4 = new Expression();
            string test4 = " r = 34567";
            string[] actResults = { "r", "34567" };
            string[] expResults = e4.Extract(test4);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_Regex_r2_output_valid_3()
        {
            Expression e4 = new Expression();
            string test4 = " r = -32";
            string[] actResults = { "r", "-32" };
            string[] expResults = e4.Extract(test4);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_Regex_r3_output_valid_1()
        {
            Expression e5 = new Expression();
            string test5 = "lastq";
            string[] actResults = { "lastq", "error" };
            string[] expResults = e5.Extract(test5);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_Regex_r4_output_valid_1()
        {
            Expression e6 = new Expression();
            string test6 = "clear a";
            string[] actResults = { "clear", "a" };
            string[] expResults = e6.Extract(test6);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_Regex_r4_output_valid_2()
        {
            Expression e7 = new Expression();
            string test7 = "remove all";
            string[] actResults = { "remove", "all" };
            string[] expResults = e7.Extract(test7);
            CollectionAssert.AreEqual(expResults, actResults);
        }
    }
}
