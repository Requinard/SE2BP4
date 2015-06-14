using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace SE2StackOverflow
{
    public partial class _Default : Page
    {
        List<Dictionary<string, string>> questions;
        protected void Page_Load(object sender, EventArgs e)
        {
            questions = new List<Dictionary<string, string>>();

            Database db = DatabaseSingleton.GetInstance();

            questions = db.GetJSONQuery("select * from indexview;");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            PostLabel.Text = "";

            foreach (var item in questions)
            {
                string text =
                    String.Format(
                        "<h2><a href='Post?post={6}'>{0}</a></h2><p>Posted by: {1} on {2}</p><p>Votes: {3} Answers: {4}</p><p>Tags: {5}</p>",
                        item["title"],
                        item["username"],
                        item["dateposted"],
                        item["votes"],
                        item["answers"],
                        item["tags"],
                        item["ident"]);

                PostLabel.Text += text;
            }
        }
    }
}