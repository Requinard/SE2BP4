// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.aspx.cs" company="">
//   
// </copyright>
// <summary>
//   The login.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SE2StackOverflow
{
    using System;
    using System.Web;
    using System.Web.UI;

    /// <summary>
    /// The login.
    /// </summary>
    public partial class Login : Page
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
            // If we're getting a POST request, we'll validate the data
            if (this.Request.HttpMethod == "POST")
            {
                int? userId = LoginLogic.Login(this.Request.Form);

                // If the uid is not null we are valid
                if (userId != null)
                {
                    // Add it to the session
                    this.Session.Add("user_id", userId.ToString());
                    this.Response.Redirect("~/");
                }
            }
        }
    }
}