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

            OracleDataReader reader = db.QueryDB("select * from indexview");

            var columns = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i).ToLower());
            } 

            while(reader.Read())
            {
                Dictionary<string,string> d = new Dictionary<string, string>();

                foreach(var column in columns)
                {
                    d.Add(column, reader[column].ToString());   
                }

                questionList.Items.Add(String.Format("{0}: {1} {2} {3}", d["ident"], d["title"], d["username"], d["dateposted"]));

                questions.Add(d);
            }
        }

        protected void questionList_Click(object sender, BulletedListEventArgs e)
        {
            string text = questionList.Items[e.Index].Value;
            Match match = Regex.Match(text,"(\\d+:)");

            if(match.Success)
            {
                // We halen nu het ID op
                string id = match.Groups[0].Value.Replace(':', ' ').Trim();
                Response.Redirect(String.Format("~/Post.aspx?post={0}", id));
            }        
        }
    }
}