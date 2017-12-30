<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyGroups.aspx.cs" Inherits="MyGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:LoginView runat="server">
        <AnonymousTemplate>
            You are not logged in!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h2>My groups</h2>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>


