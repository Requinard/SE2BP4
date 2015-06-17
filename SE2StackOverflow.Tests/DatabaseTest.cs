// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="">
//   
// </copyright>
// <summary>
//   The database test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SE2StackOverflow.Tests
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Oracle.DataAccess.Client;

    /// <summary>
    ///     The database test.
    /// </summary>
    [TestClass]
    public class DatabaseTest
    {
        /// <summary>
        ///     The singelton test.
        /// </summary>
        [TestMethod]
        public void SingeltonTest()
        {
            var db1 = DatabaseSingleton.GetInstance();

            var db2 = DatabaseSingleton.GetInstance();

            Assert.AreEqual(db1, db2, "Singleton returned different databases");
        }

        /// <summary>
        ///     The test query.
        /// </summary>
        [TestMethod]
        public void TestQuery()
        {
            var db = DatabaseSingleton.GetInstance();

            var reader = db.QueryDb("Select * from users");

            Assert.IsNotNull(reader, "Reader failed");
            Assert.IsTrue(reader.HasRows, "Reader has no rows");
        }

        /// <summary>
        ///     The test json.
        /// </summary>
        [TestMethod]
        public void TestJSON()
        {
            var db = DatabaseSingleton.GetInstance();
            var s = new List<Dictionary<string, string>>();

            var x = db.GetJsonQuery("SELECT * FROM USERS");

            Assert.IsNotNull(x, "Is null!");
            Assert.IsTrue(x.GetType() == s.GetType(), "Types did not match");
            Assert.IsTrue(x.Count > 0, "Count was below or 0");
        }

        /// <summary>
        /// The test singleton.
        /// </summary>
        [TestMethod]
        public void TestSingleton()
        {
            var db = DatabaseSingleton.GetInstance();

            var number = db.SingleIdentOperation("users", SqlOperator.Max);

            Assert.IsNotNull(number, "Number was null");
            try
            {
                Assert.IsInstanceOfType(int.Parse(number), typeof(int), "Type was not confirmed");
            }
            catch (Exception e)
            {
                Console.Write("working");
            }
        }

        [TestMethod]
        public void TestQueryFail()
        {
            Database db = DatabaseSingleton.GetInstance();

            OracleDataReader r = db.QueryDb("dawdwaforgewffaew1");

            Assert.IsNull(r, "reader returned a result");
        }

        [TestMethod]
        public void TestSingleQueryFail()
        {
            Database db = DatabaseSingleton.GetInstance();

            string r = db.SingleIdentOperation("Not existing table", SqlOperator.Max);

            Assert.IsNull(r);
        }
    }
}