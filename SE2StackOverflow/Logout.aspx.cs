namespace SE2StackOverflow
{
    using System;
    using System.Web.UI;

    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ABANDON SHIP
            // EVERYONE FOR HIMSELF
            this.Session.Abandon();
            this.Response.Redirect("~/Login.aspx");
        }
    }
}