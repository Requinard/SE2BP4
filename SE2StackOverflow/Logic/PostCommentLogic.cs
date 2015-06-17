// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostCommentLogic.cs" company="">
//   
// </copyright>
// <summary>
//   The post comment controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SE2StackOverflow
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Oracle.DataAccess.Client;

    using SE2StackOverflow.Logic;

    /// <summary>
    /// The post comment controller.
    /// </summary>
    public class PostCommentController
    {
        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="input">
        /// Form that was filled in
        /// </param>
        /// <param name="postId">
        /// ID of the post that is being commented on
        /// </param>
        /// <param name="userId">
        /// ID of the active user
        /// </param>
        /// <returns>
        /// Whether the operation wasa a success
        /// </returns>
        public static bool InsertComment(NameValueCollection input, int postId, int userId)
        {
            input = Validator.ValidateForm(input);

            Database db = DatabaseSingleton.GetInstance();
            string query = LongQueries.InsertCommentQuery(input["comment"], userId, postId);

            OracleDataReader reader = db.QueryDb(query);

            return reader != null;
        }

        /// <summary>
        /// Gets a specific post and its answers from the database
        /// </summary>
        /// <param name="postId">
        /// Post to retrieve
        /// </param>
        /// <param name="post">
        /// JSON dictionary to hold the post
        /// </param>
        /// <param name="answers">
        /// JSON dictionary to hold the answers
        /// </param>
        /// <returns>
        /// whethere there was something retrieved
        /// </returns>
        public static bool RetrievePost(
            int postId, 
            out List<Dictionary<string, string>> post, 
            out List<Dictionary<string, string>> answers)
        {
            Database db = DatabaseSingleton.GetInstance();
            post = db.GetJsonQuery(string.Format("select * from getpost where ident ='{0}'", postId));

            answers = db.GetJsonQuery(string.Format("select * from getpostcomments where postident = '{0}'", postId));

            return post != null;
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="input">
        /// Form that was filled in
        /// </param>
        /// <param name="userId">
        /// ID of the active user
        /// </param>
        /// <returns>
        /// Success of the operation
        /// </returns>
        public static bool CreateNewPost(NameValueCollection input, int userId)
        {
            input = Validator.ValidateForm(input);
            Database db = DatabaseSingleton.GetInstance();

            string query = LongQueries.InsertPostQuery(input["title"], input["body"], userId);

            OracleDataReader reader = db.QueryDb(query);

            if (reader != null)
            {
                // We take the tags an make it into a list
                string[] tags = input["tags"].Split(',');

                foreach (string item in tags)
                {
                    // We remove escapes and whitespaces
                    string tag = item.Trim();

                    // Query the db if the key exists
                    query = string.Format("SELECT * FROM TAGS WHERE name = '{0}'", tag);

                    reader = db.QueryDb(query);

                    // If it doesn't we make it
                    if (!reader.HasRows)
                    {
                        string secondQuery = string.Format(
                            "INSERT INTO TAGS (ident, name) VALUES ('{0}', '{1}');", 
                            int.Parse(db.SingleIdentOperation("tags", SqlOperator.Max)) + 1, 
                            tag);

                        reader = db.QueryDb(secondQuery);
                        reader = db.QueryDb(query);
                    }

                    if (reader == null)
                    {
                        return false;
                    }

                    reader.Read();

                    // We save the tag identity
                    string tagIdent = reader["ident"].ToString();

                    // Now we get the last inserted row from the post
                    query = string.Format("SELECT ident FROM post WHERE title = '{0}'", input["title"]);

                    reader = db.QueryDb(query);

                    reader.Read();
                    string postIdent = reader[0].ToString();

                    // We can now couple them in M2M tables
                    query =
                        string.Format(
                            "INSERT INTO POSTTAGM2M (ident, tagident, postident) VALUES ('{0}', '{1}', '{2}');", 
                            int.Parse(db.SingleIdentOperation("posttagm2m", SqlOperator.Max)) + 1, 
                            tagIdent, 
                            postIdent);

                    reader = db.QueryDb(query);
                }
            }

            return true;
        }
    }
}