namespace SE2StackOverflow
{
    using System;
    using System.Web.UI;

    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Cookies["user_id"].Value))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            var cookie = this.Request.Cookies["user_id"];
            cookie.Value = "";
            cookie.Expires = DateTime.Now - TimeSpan.FromDays(-1);
            this.Response.Cookies.Add(cookie);
            Session.Abandon();
            this.Response.Redirect("~/Login.aspx");
        }
    }
}