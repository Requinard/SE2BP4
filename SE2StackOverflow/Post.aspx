<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="SE2StackOverflow.Post" MasterPageFile="Site.Master" EnableViewState="True" enableEventValidation="False" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.Expressions" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>

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