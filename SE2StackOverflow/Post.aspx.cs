﻿namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;

    public partial class Post : Page
    {
        private List<Dictionary<string, string>> answers;

        private List<Dictionary<string, string>> post;

        private int postId;

        protected void Page_Load(object sender, EventArgs e)
        {
            string userId;

            try
            {
                userId = this.Session["user_id"].ToString();
            }
            catch (Exception)
            {
                userId = string.Empty;
            }

            this.postId = int.Parse(this.Request.QueryString["post"]);

            var db = DatabaseSingleton.GetInstance();

            // If it's a post, we insert a new comment
            if (this.Request.HttpMethod == "POST" && !string.IsNullOrEmpty(userId))
            {
                PostCommentController.InsertComment(this.Request.Form, this.postId, int.Parse(userId));
            }
            
            // haal de data op
            PostCommentController.RetrievePost(this.postId, out this.post, out this.answers);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var placeholder = this.post.First();
            this.PostLabel.Text = string.Format(
                "<h1>{0}</h1><h4>{1} - {2}</h4><p>{3}</p><p>Tagged as: <i>{4}</i></p>",
                placeholder["title"],
                placeholder["dateposted"],
                placeholder["username"],
                placeholder["postbody"],
                placeholder["tags"],
                placeholder["ident"]);

            this.AnswerLabel.Text = string.Empty;
            foreach (var ans in this.answers)
            {
                if (ans["isanswer"] == 1.ToString())
                {
                    this.AnswerLabel.Text +=
                        string.Format(
                            "<div class='panel panel-success'><div class='panel-heading'>Correct answer</div><div class='panel-body'><h4>{0}</h4><p>{1}</p></div></div>",
                            ans["username"],
                            ans["commentbody"],
                            ans["ident"]);
                }

                else
                {
                    this.AnswerLabel.Text +=
                        string.Format(
                            "<div class='panel panel-default'><div class='panel-heading'>Answer</div><div class='panel-body'><h4>{0}</h4><p>{1}</p><a href='/Correct?comment={2}'>Mark as correct</a></div></div>",
                            ans["username"],
                            ans["commentbody"],
                            ans["ident"]);
                }
            }
        }
    }
}