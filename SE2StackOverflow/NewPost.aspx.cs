namespace SE2StackOverflow
{
    using System;
    using System.Web.UI;

    public partial class NewPost : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string session;
            try
            {
                session = this.Session["user_id"].ToString();
            }
            catch (Exception)
            {
                session = null;
            }

            // If it's a post, we insert a new post
            if (this.Request.HttpMethod == "POST" && !string.IsNullOrEmpty(session))
            {
                PostCommentController.CreateNewPost(this.Request.Form, int.Parse(session));

                this.Response.Redirect("Default.aspx");
            }
        }
    }
}