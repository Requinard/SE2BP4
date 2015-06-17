namespace SE2StackOverflow
{
    using System;
    using System.Web.UI;

    public partial class Correct : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = DatabaseSingleton.GetInstance();

            string user_id;
            string comment_id;

            try
            {
                user_id = this.Session["user_id"].ToString();
                comment_id = this.Request.QueryString["comment"];
            }
            catch (Exception)
            {
                user_id = null;
                comment_id = null;
            }

            if (!string.IsNullOrEmpty(user_id) && !string.IsNullOrEmpty(comment_id))
            {
                var comment =
                    db.GetJSONQuery(
                        string.Format(
                            "SELECT postcomment.ident as commentident, post.ident as postident, post.userident as userident FROM POSTCOMMENT JOIN POST ON (postcomment.postident = post.ident) WHERE postcomment.ident = '{0}'",
                            comment_id));

                if (comment[0]["userident"] == user_id)
                {
                    var query = string.Format("update postcomment set isanswer = 1 where ident = '{0}'", comment_id);

                    var reader = db.QueryDB(query);

                    if (reader != null)
                    {
                        this.Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    this.Label1.Text = "Only the original poster can mark an answer as correct";
                }
            }
        }
    }
}