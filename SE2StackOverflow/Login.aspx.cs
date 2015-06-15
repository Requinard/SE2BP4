using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE2StackOverflow
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                int? user_id = LoginLogic.Login(Request.Form);

                if (user_id != null)
                {
                    Session.Add("user_id", user_id.ToString());
                    HttpCookie cookie = new HttpCookie("user_id", user_id.ToString());
                    cookie.Expires.AddDays(1);
                    Response.Cookies.Add(cookie);
                    Response.Redirect("~/");
                }
            }
        }
    }
}