namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using SE2StackOverflow.Logic;

    public class PostCommentController
    {
        public static bool InsertComment(NameValueCollection input, int post_id, int user_id)
        {
            Validator.ValidateForm(input);

            var db = DatabaseSingleton.GetInstance();
            var query = LongQueries.InsertCommentQuery(input["comment"], user_id, post_id);

            var reader = db.QueryDB(query);

            return reader != null;
        }

        public static bool RetrievePost(
            int post_id,
            out List<Dictionary<string, string>> post,
            out List<Dictionary<string, string>> answers)
        {
            var db = DatabaseSingleton.GetInstance();
            post = db.GetJSONQuery(string.Format("select * from getpost where ident ='{0}'", post_id));

            answers = db.GetJSONQuery(string.Format("select * from getpostcomments where postident = '{0}'", post_id));

            return post != null;
        }

        public static bool CreateNewPost(NameValueCollection input, int user_id)
        {
            input = Validator.ValidateForm(input);
            var db = DatabaseSingleton.GetInstance();

            var query = LongQueries.InsertPostQuery(input["title"], input["body"], user_id);

            var reader = db.QueryDB(query);

            if (reader != null)
            {
                // We take the tags an make it into a list
                var tags = input["tags"].Split(',');

                foreach (var item in tags)
                {
                    // We remove escapes and whitespaces
                    var tag = item.Trim();

                    // Query the db if the key exists
                    query = string.Format("SELECT * FROM TAGS WHERE name = '{0}'", tag);

                    reader = db.QueryDB(query);

                    // If it doesn't we make it
                    if (!reader.HasRows)
                    {
                        var second_query = string.Format(
                            "INSERT INTO TAGS (ident, name) VALUES ('{0}', '{1}');",
                            Int32.Parse(db.SingleIdentOperation("tags", SQLOperator.MAX))+ 1,
                            tag);

                        reader = db.QueryDB(second_query);
                        reader = db.QueryDB(query);
                    }

                    if (reader == null)
                    {
                        return false;
                    }

                    reader.Read();

                    //We save the tag identity
                    var tag_ident = reader["ident"].ToString();

                    // Now we get the last inserted row from the post
                    query = string.Format("SELECT ident FROM post WHERE title = '{0}'", input["title"]);

                    reader = db.QueryDB(query);

                    reader.Read();
                    var post_ident = reader[0].ToString();

                    // We can now couple them in M2M tables

                    query =
                        string.Format(
                            "INSERT INTO POSTTAGM2M (ident, tagident, postident) VALUES ('{0}', '{1}', '{2}');",
                            int.Parse(db.SingleIdentOperation("posttagm2m", SQLOperator.MAX))+1,
                            tag_ident,
                            post_ident);

                    reader = db.QueryDB(query);
                }
            }

            return true;
        }
    }
}