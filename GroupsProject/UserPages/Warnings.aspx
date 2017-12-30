<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Warnings.aspx.cs" Inherits="UserPages_Warnings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LoginView runat="server">
        <AnonymousTemplate>
            You are not logged in!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h2 style="color: red">Warnings</h2>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

