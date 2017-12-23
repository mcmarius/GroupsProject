<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Home</h2>
    
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>sign up/sign in</AnonymousTemplate>
        <LoggedInTemplate>hi,<asp:LoginName ID="LoginName1" runat="server" /> </LoggedInTemplate>
    </asp:LoginView>
    
    
    
    <asp:GridView runat="server" DataSourceID="SqlDataSource"></asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ConnectionStrings:ConnectionString%>" SelectCommand="SELECT CategoryName FROM Categories"></asp:SqlDataSource>
</asp:Content>

