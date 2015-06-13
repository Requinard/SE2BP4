using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE2StackOverflow
{
    public partial class Post : System.Web.UI.Page
    {
        private int post_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            post_id = Int32.Parse(Request.QueryString["post"]);
        }
    }
}