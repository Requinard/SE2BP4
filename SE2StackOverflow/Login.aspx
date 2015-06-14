<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SE2StackOverflow.Login" MasterPageFile="Site.Master" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1>Een nieuwe vraag stellen</h1>
            <div class="row">
                <form class="form-horizontal" method="POST">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Gebruikersnaam</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" name="username" required/>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-sm-2">Wachtwoord</label>
                        <div class="col-sm-10">
                            <input type="password" required name="password" class="form-control"/>
                        </div>
                    </div>

                    <button type="submit" class="btn btn-success btn-block">Log in</button>
                </form>
            </div>
        </div>
    </div>


</asp:Content>