using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE2StackOverflow
{
    using System.Web.WebPages;

    public partial class Post : System.Web.UI.Page
    {
        private int post_id;

        private List<Dictionary<string, string>> post;
        private List<Dictionary<string, string>> answers;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                post_id = Int32.Parse(Request.QueryString["post"]);
            }
            catch (Exception)
            {
                post_id = 1;
            }
            Database db = DatabaseSingleton.GetInstance();

            if (post_id == null) post_id = 1;
            post = db.GetJSONQuery(String.Format("select * from getpost where ident ='{0}'", post_id));

            answers = db.GetJSONQuery(String.Format("select * from getpostcomments where postident = '{0}'", post_id));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Dictionary<string, string> placeholder = post.First();
            PostLabel.Text = String.Format("<h1>{0}</h1><h4>{1} - {2}</h4><p>{3}</p>", placeholder["title"], placeholder["dateposted"], placeholder["username"], placeholder["postbody"]);

            AnswerLabel.Text = "";
            foreach (var ans in answers)
            {
                AnswerLabel.Text += String.Format("<h4>{0}</h4><p>{1}</p>", ans["username"], ans["commentbody"]);
            }


        } 
    }
}