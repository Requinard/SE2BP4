using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE2StackOverflow
{
    public partial class NewPost : System.Web.UI.Page
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

            if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(session))
            {
                PostCommentController.CreateNewPost(Request.Form, Int32.Parse(session));

                Response.Redirect("Default.aspx");
            }
        }
    }
}