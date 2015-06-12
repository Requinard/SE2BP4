<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SE2StackOverflow._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 

    <asp:BulletedList ID="questionList" runat="server" OnClick="questionList_Click">
    </asp:BulletedList>

 

</asp:Content>
