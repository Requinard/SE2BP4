// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logout.aspx.cs" company="">
//   
// </copyright>
// <summary>
//   The logout.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SE2StackOverflow
{
    using System;
    using System.Web.UI;

    /// <summary>
    ///     The logout.
    /// </summary>
    public partial class Logout : Page
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
            // ABANDON SHIP
            // EVERYONE FOR HIMSELF
            this.Session.Abandon();
            this.Response.Redirect("~/Login.aspx");
        }
    }
}