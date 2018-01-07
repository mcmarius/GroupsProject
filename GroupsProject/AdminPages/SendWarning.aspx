<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SendWarning.aspx.cs" Inherits="AdminPages.SendWarning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Literal runat="server" ID="StatusMsg"></asp:Literal>
    <asp:Label runat="server" ID="WarnLabel" Text="Warning Message"></asp:Label>
    <asp:TextBox runat="server" ID="WarnMessage"></asp:TextBox>
    <asp:Button runat="server" ID="WarnButton" Text="Send warning!" OnClick="WarnButton_OnClick"/>
</asp:Content>

