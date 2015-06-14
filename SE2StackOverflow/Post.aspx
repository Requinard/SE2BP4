<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="SE2StackOverflow.Post" MasterPageFile="Site.Master" EnableViewState="True" enableEventValidation="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="PostLabel" runat="server" Text="Hier komt de titel"></asp:Label>
        <hr/>
        <h5>Post een antwoord</h5>
        <form >
            <textarea name="comment" id="TextArea1" cols="20" rows="2" class="form-control"></textarea>
            <br/>
            <button class="btn btn-primary">submit</button>
        </form>

        <hr/>
        <asp:Label ID="AnswerLabel" runat="server" Text="Answers"></asp:Label>
    </div>
</asp:Content>