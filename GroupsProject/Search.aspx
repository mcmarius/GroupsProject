﻿<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Search groups and users</h2>
    
    <asp:Label ID="SearchLabel" runat="server" Text="Search: "></asp:Label>
    <asp:TextBox ID="SearchTextBox" runat="server"></asp:TextBox>
    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_OnClick"/>
    <br/>
    <asp:CheckBox runat="server" ID="StrictCB" Text="Exact match (e.g. for emojis)"/>
    <br/>
    <asp:Repeater runat="server" ID="MyRepeater" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div style="padding: 10px">
                <h3> <%# DataBinder.Eval(Container.DataItem, "GroupName") %> </h3>

                <div>
                    Description: <%# DataBinder.Eval(Container.DataItem, "GroupDescription") %>
                </div>
                <div>
                    Category: <%# DataBinder.Eval(Container.DataItem, "CategoryName") %>
                </div>

                <asp:HyperLink runat="server" ID="HLGroup"
                               NavigateUrl='<%# "~/GroupPage.aspx?gid=" + Server.UrlEncode(DataBinder.Eval(Container.DataItem, "GroupId").ToString()) %>'>Group page</asp:HyperLink>
            </div>
        </ItemTemplate>

    </asp:Repeater>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                       
                       SelectCommand="SELECT DISTINCT Groups.GroupId, Groups.GroupName, Groups.GroupDescription, CategoryName FROM [Groups]
                                              INNER JOIN [GroupsLists] ON Groups.GroupId = GroupsLists.GroupId
                                              INNER JOIN [Categories] ON Groups.CategoryId = Categories.CategoryId">
    </asp:SqlDataSource>
</asp:Content>


