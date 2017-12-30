<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="AdminPages_Confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LoginView runat="server">
        <AnonymousTemplate>
            You are not logged in!
        </AnonymousTemplate>
        <RoleGroups>
            <asp:RoleGroup runat="server" Roles="Admin">
                <ContentTemplate>
                    Are you sure?
                    <>br>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Yes" />
                    
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
</asp:Content>

