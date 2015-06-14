namespace SE2StackOverflow
{
    using System;
    using System.Text;

    public class LongQueries
    {
        public static string InsertCommentQuery(string comment, int user_id, int post_id)
        {
            var db = DatabaseSingleton.GetInstance();

            var SQL = new StringBuilder();
            SQL.Append("INSERT ");
            SQL.Append("INTO POSTCOMMENT");
            SQL.Append("  ( ");
            SQL.Append("    IDENT, ");
            SQL.Append("    USERIDENT, ");
            SQL.Append("    POSTIDENT, ");
            SQL.Append("    COMMENTBODY, ");
            SQL.Append("    DATEPOST ");
            SQL.Append("  ) ");
            SQL.Append("  VALUES ");
            SQL.Append("  ( ");
            SQL.Append(string.Format("'{0}',", int.Parse(db.SingleIdentOperation("postcomment", SQLOperator.MAX)) + 1));
            SQL.Append(string.Format("'{0}',", user_id));
            SQL.Append(string.Format("'{0}',", post_id));

            SQL.Append(string.Format("'{0}',", comment));
            SQL.Append(string.Format("TO_DATE('{0}', 'MM/DD/YYYY HH24:MI:SS') ", DateTime.Now));
            SQL.Append("  ); ");
            SQL.Append("COMMIT;");

            return SQL.ToString();
        }

        public static string InsertPostQuery(string title, string post, int user_id)
        {
            var db = DatabaseSingleton.GetInstance();

            var SQL = new StringBuilder();

            SQL.Append("INSERT ");
            SQL.Append("INTO POST ");
            SQL.Append("  ( ");
            SQL.Append("    IDENT, ");
            SQL.Append("    TITLE, ");
            SQL.Append("    POSTBODY, ");
            SQL.Append("    DATEPOSTED, ");
            SQL.Append("    COMMUNITYIDENT, ");
            SQL.Append("    USERIDENT ");
            SQL.Append("  ) ");
            SQL.Append("  VALUES ");
            SQL.Append("  ( ");
            SQL.Append(string.Format("'{0}',", int.Parse(db.SingleIdentOperation("post", SQLOperator.MAX)) + 1));
            SQL.Append(string.Format("'{0}',", title));
            SQL.Append(string.Format("'{0}',", post));
            SQL.Append(string.Format("TO_DATE('{0}', 'MM/DD/YYYY HH24:MI:SS'), ", DateTime.Now));
            SQL.Append("    '1', ");
            SQL.Append(string.Format("'{0}'", user_id));
            SQL.Append("  );");

            return SQL.ToString();
        }
    }
}