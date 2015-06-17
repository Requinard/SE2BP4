// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Site.Master.cs" company="">
//   
// </copyright>
// <summary>
//   The site master.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SE2StackOverflow
{
    using System;
    using System.Web.UI;

    /// <summary>
    ///     The site master.
    /// </summary>
    public partial class SiteMaster : MasterPage
    {
        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The text box 1 text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void TextBox1TextChanged(object sender, EventArgs e)
        {
            this.Response.Redirect(string.Format("~/Search.aspx?query={0}", this.TextBox1.Text.Replace(" ", "+")));
        }
    }
}