using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2StackOverflow
{
    using System.Collections.Specialized;

    using Oracle.DataAccess.Client;

    public class PostCommentController
    {
        public static bool InsertComment(NameValueCollection input, int post_id, int user_id)
        {
            Database db = DatabaseSingleton.GetInstance();
            string query = LongQueries.InsertCommentQuery(input["comment"], user_id, post_id);

            OracleDataReader reader = db.QueryDB(query);

            return reader != null;
        }

        public static bool RetrievePost(
            int post_id,
            out List<Dictionary<string, string>> post,
            out List<Dictionary<string, string>> answers)
        {
            Database db = DatabaseSingleton.GetInstance();
            post = db.GetJSONQuery(string.Format("select * from getpost where ident ='{0}'", post_id));

            answers = db.GetJSONQuery(string.Format("select * from getpostcomments where postident = '{0}'", post_id));

            return post != null;
        }
    }
}