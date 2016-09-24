using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using System.Collections.Generic;
using SavingVariables.Models;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace SavingVariables.Tests
{
    [TestClass]
    public class UnitTest1
    {

        Mock<VarContext> mock_context { get; set; }
        Mock<DbSet<SaveVars>> mock_savevars_table { get; set; }
        List<SaveVars> savevars_list { get; set; }
        VarRepository repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            var queryable_list = savevars_list.AsQueryable();

            mock_savevars_table.As<IQueryable<SaveVars>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_savevars_table.As<IQueryable<SaveVars>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_savevars_table.As<IQueryable<SaveVars>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_savevars_table.As<IQueryable<SaveVars>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            mock_context.Setup(c => c.Vars).Returns(mock_savevars_table.Object);

            mock_savevars_table.Setup(t => t.Add(It.IsAny<SaveVars>())).Callback((SaveVars a) => savevars_list.Add(a));
            mock_savevars_table.Setup(t => t.Remove(It.IsAny<SaveVars>())).Callback((SaveVars a) => savevars_list.Remove(a));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<VarContext>();
            mock_savevars_table = new Mock<DbSet<SaveVars>>();
            savevars_list = new List<SaveVars>();
            repo = new VarRepository(mock_context.Object);

            ConnectMocksToDatastore();
        }

        [TestCleanup]
        public void TearDown()
        {
            repo = null;
        }

        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            VarRepository repo = new VarRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureRepoHasContext()
        {
            VarRepository repo = new VarRepository();
            VarContext actual_context = repo.Context;
            Assert.IsInstanceOfType(actual_context, typeof(VarContext));
        }

        [TestMethod]
        public void RepoEnsureWeHaveNoVars()
        {
            List<SaveVars> actual_vars = repo.GetVars();
            int expected_vars_count = 0;
            int actual_vars_count = actual_vars.Count();
            Assert.AreEqual(expected_vars_count, actual_vars_count);
        }

        [TestMethod]
        public void RepoAddVarToDatabase()
        {
            SaveVars my_var = new SaveVars { VarId = 1, VarName = "s", Value = 8 };
            repo.AddVars(my_var);
            int actual_var_count = repo.GetVars().Count;
            int expected_var_count = 1;
            Assert.AreEqual(expected_var_count, actual_var_count);
        }

        [TestMethod]
        public void RepoEnsureAddVarWithArgs()
        {
            repo.AddVars(1, "x", 32);
            List<SaveVars> actual_var = repo.GetVars();
            string actual_var_name = actual_var.First().VarName;
            string expected_var_name = "x";

            Assert.AreEqual(expected_var_name, actual_var_name);
        }

        [TestMethod]
        public void RepoEnsureFindVarByVarName()
        {
            // Arrange
            savevars_list.Add(new SaveVars { VarId = 1, VarName = "q", Value = 21 });
            savevars_list.Add(new SaveVars { VarId = 2, VarName = "t", Value = 109 });
            savevars_list.Add(new SaveVars { VarId = 3, VarName = "g", Value = 54 });

            // Act
            string vName = "t";
            SaveVars actual_var = repo.FindVarByVarName(vName);

            // Assert
            int expected_var_id = 2;
            int actual_var_id = actual_var.VarId;
            Assert.AreEqual(expected_var_id, actual_var_id);
        }

        [TestMethod]
        public void RepoEnsureFindValueByVarName()
        {
            // Arrange
            savevars_list.Add(new SaveVars { VarId = 1, VarName = "q", Value = 21 });
            savevars_list.Add(new SaveVars { VarId = 2, VarName = "t", Value = 109 });
            savevars_list.Add(new SaveVars { VarId = 3, VarName = "g", Value = 54 });

            // Act
            string vName = "t";
            SaveVars actual_var = repo.FindVarByVarName(vName);

            // Assert
            int expected_var_value = 109;
            int actual_var_value = actual_var.Value;
            Assert.AreEqual(expected_var_value, actual_var_value);
        }

        [TestMethod]
        public void RepoEnsureICanRemoveVar()
        {
            // Arrange
            // Arrange
            savevars_list.Add(new SaveVars { VarId = 1, VarName = "q", Value = 21 });
            savevars_list.Add(new SaveVars { VarId = 2, VarName = "t", Value = 109 });
            savevars_list.Add(new SaveVars { VarId = 3, VarName = "g", Value = 54 });

            // Act
            string var_name = "t";
            SaveVars removed_var = repo.RemoveVar(var_name);
            int expected_var_count = 2;
            int actual_var_count = repo.GetVars().Count;
            int expected_var_id = 2;
            int actual_var_id = removed_var.VarId;
            // Assert
            Assert.AreEqual(expected_var_count, actual_var_count);
            Assert.AreEqual(expected_var_id, actual_var_id);
        }

        [TestMethod]
        public void RepoEnsureICanNotRemoveThingsNotThere()
        {
            // Arrange
            savevars_list.Add(new SaveVars { VarId = 1, VarName = "q", Value = 21 });
            savevars_list.Add(new SaveVars { VarId = 2, VarName = "t", Value = 109 });
            savevars_list.Add(new SaveVars { VarId = 3, VarName = "g", Value = 54 });

            // Act
            string var_name = "h";
            SaveVars removed_var = repo.RemoveVar(var_name);

            // Assert
            Assert.IsNull(removed_var);
        }

        [TestMethod]
        public void Test_Instance()
        {
            Expression e0 = new Expression();
            Assert.IsNotNull(e0);
        }

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

        [TestMethod]
        public void Test_quit()
        {
            Expression e8 = new Expression();
            string test8 = "quit";
            string[] actResults = { "quit", "error" };
            string[] expResults = e8.Extract(test8);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_exit()
        {
            Expression e9 = new Expression();
            string test9 = "exit";
            string[] actResults = { "exit", "error" };
            string[] expResults = e9.Extract(test9);
            CollectionAssert.AreEqual(expResults, actResults);
        }

        [TestMethod]
        public void Test_lastq()
        {
            Expression e8 = new Expression();
            string test10a = "lastq";
            string[] test10b = { "lastq", "error" };
            string output10a = e8.Process(test10b, "lastq");
            string expResults = e8.Process(test10b, "lastq");
            Assert.AreEqual(expResults, test10a);
        }
    }
}
