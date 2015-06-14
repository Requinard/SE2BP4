﻿namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    public partial class Post : Page
    {
        private List<Dictionary<string, string>> answers;

        private List<Dictionary<string, string>> post;

        private int post_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["user_id"];
            this.post_id = int.Parse(this.Request.QueryString["post"]);

            var db = DatabaseSingleton.GetInstance();

            if (this.Request.HttpMethod == "POST" && !string.IsNullOrEmpty(cookie.Value))
            {
                PostCommentController.InsertComment(this.Request.Form, this.post_id, Int32.Parse(cookie.Value));
            }

            PostCommentController.RetrievePost(post_id, out this.post, out this.answers);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var placeholder = this.post.First();
            this.PostLabel.Text = string.Format(
                "<h1>{0}</h1><h4>{1} - {2}</h4><p>{3}</p>",
                placeholder["title"],
                placeholder["dateposted"],
                placeholder["username"],
                placeholder["postbody"]);

            this.AnswerLabel.Text = "";
            foreach (var ans in this.answers)
            {
                this.AnswerLabel.Text += string.Format("<h4>{0}</h4><p>{1}</p>", ans["username"], ans["commentbody"]);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Console.Write("test");
        }
    }
}