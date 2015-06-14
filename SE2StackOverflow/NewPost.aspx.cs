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
            HttpCookie cookie = Request.Cookies["user_id"];

            if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(cookie.Value))
            {
                PostCommentController.CreateNewPost(Request.Form, Int32.Parse(cookie.Value));

                Response.Redirect("Default.aspx");
            }
        }
    }
}