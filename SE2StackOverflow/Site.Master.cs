namespace SE2StackOverflow
{
    using System;
    using System.Web.UI;

    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void TextBox1TextChanged(object sender, EventArgs e)
        {
            this.Response.Redirect(string.Format("~/Search.aspx?query={0}", this.TextBox1.Text.Replace(" ", "+")));
        }
    }
}