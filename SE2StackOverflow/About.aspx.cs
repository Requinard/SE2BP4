using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE2StackOverflow
{
    public partial class About : Page
    {
        List<Dictionary<string, string>> questions;

        protected void Page_Load(object sender, EventArgs e)
        {
            questions = new List<Dictionary<string, string>>();

            Database db = DatabaseSingleton.GetInstance();


        }
    }
}