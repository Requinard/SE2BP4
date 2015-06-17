// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostCommentController.cs" company="">
//   
// </copyright>
// <summary>
//   The post comment logic test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SE2StackOverflow.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The post comment logic test.
    /// </summary>
    [TestClass]
    public class PostCommentLogicTest
    {
        /// <summary>
        /// The create new post.
        /// </summary>
        [TestMethod]
        public void CreateNewPost()
        {
            var nv = new NameValueCollection();

            nv["title"] = "Dit is een test title";
            nv["body"] = "Dit is een test body";
            nv["tags"] = "C#, asp.net";
            var r = new Random();
            nv["tags"] += new string(Enumerable.Range(0, 10).Select(n => (char)r.Next(32, 127)).ToArray());

            var success = PostCommentController.CreateNewPost(nv, 1);

            Assert.IsTrue(success, "could not create comment");
        }

        /// <summary>
        /// The create new comment.
        /// </summary>
        [TestMethod]
        public void CreateNewComment()
        {
            var nv = new NameValueCollection();

            nv["comment"] = "bla bla";

            var success = PostCommentController.InsertComment(nv, 1, 1);

            Assert.IsTrue(success);
        }

        /// <summary>
        /// The retrieve commets.
        /// </summary>
        [TestMethod]
        public void RetrieveCommets()
        {
            List<Dictionary<string, string>> post;
            List<Dictionary<string, string>> answers;

            var x = PostCommentController.RetrievePost(1, out post, out answers);

            Assert.IsNotNull(post);
            Assert.IsTrue(post.Count == 1 || post.Count == 0, "Retrieved more posts then possible");

            Assert.IsNotNull(answers);
            Assert.IsTrue(answers.Count > 0, "did not receive enough comments");
        }
    }
}