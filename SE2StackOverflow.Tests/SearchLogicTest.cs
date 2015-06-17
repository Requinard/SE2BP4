// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchLogicTest.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for SearchLogicTest
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SE2StackOverflow.Tests
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SE2StackOverflow.Logic;

    /// <summary>
    ///     Summary description for SearchLogicTest
    /// </summary>
    [TestClass]
    public class SearchLogicTest
    {
        /// <summary>
        /// The search.
        /// </summary>
        [TestMethod]
        public void Search()
        {
            var nv = new NameValueCollection();

            nv["query"] = "t";

            List<Dictionary<string, string>> x = SearchLogic.Search(nv);

            Assert.IsNotNull(x);
            Assert.IsTrue(x.Count > 0);
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        #endregion
    }
}