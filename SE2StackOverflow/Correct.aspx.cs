﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Correct.aspx.cs" company="">
//   
// </copyright>
// <summary>
//   Marks an answer as correct
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    using Oracle.DataAccess.Client;

    /// <summary>
    /// Marks an answer as correct
    /// </summary>
    public partial class Correct : Page
    {
        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Database db = DatabaseSingleton.GetInstance();

            string userId;
            string commentId;
            
            // try to read the user id and comment id
            try
            {
                userId = this.Session["user_id"].ToString();
                commentId = this.Request.QueryString["comment"];
            }
            catch (Exception)
            {
                userId = null;
                commentId = null;
            }

            // If both values are valid we run a query
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(commentId))
            {
                // First we get the parent comment to validate that the user is the original poster
                List<Dictionary<string, string>> comment =
                    db.GetJsonQuery(
                        string.Format(
                            "SELECT postcomment.ident as commentident, post.ident as postident, post.userident as userident FROM POSTCOMMENT JOIN POST ON (postcomment.postident = post.ident) WHERE postcomment.ident = '{0}'", 
                            commentId));

                // If the user is the OP, we can continue
                if (comment[0]["userident"] == userId)
                {
                    // Create query and execute it
                    string query = string.Format("update postcomment set isanswer = 1 where ident = '{0}'", commentId);

                    OracleDataReader reader = db.QueryDb(query);

                    // If it was a success we redirect
                    if (reader != null)
                    {
                        this.Response.Redirect(string.Format("~/Post?post={0}", comment[0]["postident"]));
                    }
                }
                else
                {
                    // Since we have a user and a comment, it can only be that someone is illegitemately marking it correct!
                    this.Label1.Text = "Only the original poster can mark an answer as correct";
                }
            }
        }
    }
}