// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Search.aspx.cs" company="">
//   
// </copyright>
// <summary>
//   The search.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web.UI;

    using SE2StackOverflow.Logic;

    /// <summary>
    ///     The search.
    /// </summary>
    public partial class Search : Page
    {
        /// <summary>
        ///     The results.
        /// </summary>
        private List<Dictionary<string, string>> results;

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
            // Vervang de inkomende data en zet deze in een name valuecollections
            var query = this.Request.QueryString["query"].Replace("+", " ");
            var v = new NameValueCollection();
            v["query"] = query;

            // Valideer deze
            v = Validator.ValidateForm(v);

            // sla de query op
            this.results = SearchLogic.Search(v);
        }

        /// <summary>
        /// The page_ pre render.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.Content.Text = string.Empty;

            foreach (var item in this.results)
            {
                this.Content.Text += string.Format(
                    "<h2><a href='/Post?post={0}'>{1}</a></h2><p>Posted on {2}", 
                    item["ident"], 
                    item["title"], 
                    item["dateposted"]);
            }
        }
    }
}