namespace SE2StackOverflow
{
    using System;
    using System.Text;

    // Generates reaaaaaaally long queries
    public class LongQueries
    {
        /// <summary>
        /// Get the SQL for inserting a comment
        /// </summary>
        /// <param name="comment">textbody of the comment</param>
        /// <param name="userId">ID of the posting user</param>
        /// <param name="postId">ID of the parent comment</param>
        /// <returns>SQL query to execute</returns>
        public static string InsertCommentQuery(string comment, int userId, int postId)
        {
            var db = DatabaseSingleton.GetInstance();

            var sql = new StringBuilder();
            sql.Append("INSERT ");
            sql.Append("INTO POSTCOMMENT");
            sql.Append("  ( ");
            sql.Append("    IDENT, ");
            sql.Append("    USERIDENT, ");
            sql.Append("    POSTIDENT, ");
            sql.Append("    COMMENTBODY, ");
            sql.Append("    DATEPOST ");
            sql.Append("  ) ");
            sql.Append("  VALUES ");
            sql.Append("  ( ");
            sql.Append(string.Format("'{0}',", int.Parse(db.SingleIdentOperation("postcomment", SqlOperator.Max)) + 1));
            sql.Append(string.Format("'{0}',", userId));
            sql.Append(string.Format("'{0}',", postId));

            sql.Append(string.Format("'{0}',", comment));
            sql.Append(string.Format("TO_DATE('{0}', 'MM/DD/YYYY HH24:MI:SS') ", DateTime.Now));
            sql.Append("  ); ");
            sql.Append("COMMIT;");

            return sql.ToString();
        }

        /// <summary>
        /// Creates the query to create a new post
        /// </summary>
        /// <param name="title">Title of the pos</param>
        /// <param name="post">Body of the post</param>
        /// <param name="userId">ID of the active user</param>
        /// <returns>SQL query that can be executed</returns>
        public static string InsertPostQuery(string title, string post, int userId)
        {
            var db = DatabaseSingleton.GetInstance();

            var sql = new StringBuilder();

            sql.Append("INSERT ");
            sql.Append("INTO POST ");
            sql.Append("  ( ");
            sql.Append("    IDENT, ");
            sql.Append("    TITLE, ");
            sql.Append("    POSTBODY, ");
            sql.Append("    DATEPOSTED, ");
            sql.Append("    COMMUNITYIDENT, ");
            sql.Append("    USERIDENT ");
            sql.Append("  ) ");
            sql.Append("  VALUES ");
            sql.Append("  ( ");
            sql.Append(string.Format("'{0}',", int.Parse(db.SingleIdentOperation("post", SqlOperator.Max)) + 1));
            sql.Append(string.Format("'{0}',", title));
            sql.Append(string.Format("'{0}',", post));
            sql.Append(string.Format("TO_DATE('{0}', 'MM/DD/YYYY HH24:MI:SS'), ", DateTime.Now));
            sql.Append("    '1', ");
            sql.Append(string.Format("'{0}'", userId));
            sql.Append("  );");

            return sql.ToString();
        }
    }
}