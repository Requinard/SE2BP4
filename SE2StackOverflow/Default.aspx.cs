﻿namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class Default : Page
    {
        private List<Dictionary<string, string>> questions;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Select the questions
            this.questions = new List<Dictionary<string, string>>();

            var db = DatabaseSingleton.GetInstance();

            this.questions = db.GetJsonQuery("select * from indexview;");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.PostLabel.Text = "";

            // Render the existing dictionary
            foreach (var item in this.questions)
            {
                var text =
                    string.Format(
                        "<h2><a href='Post?post={6}'>{0}</a></h2><p>Posted by: {1} on {2}</p><p>Votes: {3} Answers: {4}</p><p>Tags: {5}</p>",
                        item["title"],
                        item["username"],
                        item["dateposted"],
                        item["votes"],
                        item["answers"],
                        item["tags"],
                        item["ident"]);

                this.PostLabel.Text += text;
            }
        }
    }
}