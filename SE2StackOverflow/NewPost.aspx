<%@ Page Title="New Post" Language="C#" MasterPageFile="Site.Master" CodeBehind="NewPost.aspx.cs" Inherits="SE2StackOverflow.NewPost" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1>Een nieuwe vraag stellen</h1>
            <form class="form-horizontal" method="POST">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Titel</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" name="title"/>
                    </div>

                </div>
                <div class="form-group">
                    <label class="col-sm-2">Stel hier de rest van je vraag op</label>
                    <div class="col-sm-10">
                        <textarea cols="60" rows="6" class="form-control" name="body"></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Eventuele tags (gescheiden door comma's)</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" name="tags" placeholder="C#, ASP.NET, Docker"/>
                    </div>
                </div>

                <button type="submit" class="btn btn-success">Invoeren</button>
            </form>
        </div>
    </div>


</asp:Content>