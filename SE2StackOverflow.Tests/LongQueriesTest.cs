using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SE2StackOverflow.Tests
{
    [TestClass]
    public class LongQueriesTest
    {
        [TestMethod]
        public void PostQuery()
        {
            string x = LongQueries.InsertPostQuery("Testing Title", "this is a post", 1);

            Assert.IsNotNull(x, "String was null");
            Assert.IsTrue(x.Length > 0, "string is 0");
        }

        [TestMethod]
        public void CommentQueryTest()
        {
            string x = LongQueries.InsertCommentQuery("Dit is commentaar", 1, 1);

            Assert.IsNotNull(x, "String was null");
            Assert.IsTrue(x.Length > 0, "string is 0"); 
        }
    }
}
