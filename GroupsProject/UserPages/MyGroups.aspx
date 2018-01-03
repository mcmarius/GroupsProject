<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyGroups.aspx.cs" Inherits="MyGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:LoginView runat="server" ID="LVMG">
        <AnonymousTemplate>
            You are not logged in!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h2>My groups</h2>
            
            <asp:GridView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" CellPadding="10" BorderColor="White"></asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                               OnSelecting="SqlDataSource1_OnSelecting"
                               SelectCommand="SELECT [GroupName] AS 'Group Name', [GroupDescription] AS 'Description' FROM [Groups]
                                              INNER JOIN [GroupsLists] ON Groups.GroupId = GroupsLists.GroupId
                                              WHERE UserName = @UserName"
                               >
                <SelectParameters>
                    <asp:Parameter Name="UserName"/>
                </SelectParameters>
            </asp:SqlDataSource>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>


