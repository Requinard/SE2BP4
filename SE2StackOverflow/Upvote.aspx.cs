using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE2StackOverflow
{
    using System.Collections.Specialized;

    public partial class Upvote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.QueryString["type"])
            {
                case "post":
                    UpvotePost();
                    break;
                case "comment":
                    UpvoteComment();
                    break;
            }

        }

        private void UpvoteComment()
        {
            string post_id = Request.QueryString["id"];
            string votecount = Request.QueryString["positive"];
            int count = 0;

            // Set count
            if (votecount == "1")
            {
                count = 1;
            }
            else if (votecount == "0")
            {
                count = -1;
            }

            // If the count is the same, we'll exit the function
            if (count == 0)
            {
                return;
            }

            // Now we can update
            


        }


        private void UpvotePost()
        {
            throw new NotImplementedException();
        }
    }
}