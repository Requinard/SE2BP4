using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                questionList.Items.Add(String.Format("{4} {0} {1} {2}", d["title"], d["username"], d["dateposted"], d["ident"]));

                questions.Add(d);
            }
        }

        protected void questionList_Click(object sender, BulletedListEventArgs e)
        {
            
        }
    }
}