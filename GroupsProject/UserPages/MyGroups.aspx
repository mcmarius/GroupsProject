<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyGroups.aspx.cs" Inherits="UserPages.MyGroups" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>
        <asp:Literal runat="server" ID="TitleLiteral" Text="My groups"></asp:Literal>
    </title>
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:LoginView runat="server" ID="LVMG">
        <AnonymousTemplate>
            You are not logged in!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h2>My groups</h2>

            <%--<asp:GridView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" CellPadding="10" BorderColor="White">
                <Columns>
                    <asp:BoundField DataField="Group Name" HeaderText="Group name"/>
                    <%- -<asp:TemplateField>
                        <ItemTemplate>

                            <asp:HyperLink runat="server" ID="HLGroup"
                                           NavigateUrl='<% var ds = LVMG.FindControl("SqlDataSource1");
                                                           string gid = ds; %>
                                <% "~/GroupPage.aspx?gid=" + Server.UrlEncode(gid) %>'>Group page</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField> -- %>
                </Columns>
            </asp:GridView>--%>

            <asp:Repeater runat="server" ID="MyRepeater" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div style="padding: 10px">
                        <h3> <%# DataBinder.Eval(Container.DataItem, "Group Name") %> </h3>

                        <div>
                            Description: <%# DataBinder.Eval(Container.DataItem, "Description") %>
                        </div>

                        <asp:HyperLink runat="server" ID="HLGroup"
                                       NavigateUrl='<%# "~/GroupPage.aspx?gid=" + Server.UrlEncode(DataBinder.Eval(Container.DataItem, "GID").ToString()) %>'>Group page</asp:HyperLink>
                    </div>
                </ItemTemplate>

            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                               OnSelecting="SqlDataSource1_OnSelecting"
                               SelectCommand="SELECT Groups.GroupId AS 'GID', Groups.GroupName AS 'Group Name', Groups.GroupDescription AS 'Description' FROM [Groups]
                                              INNER JOIN [GroupsLists] ON Groups.GroupId = GroupsLists.GroupId
                                              WHERE UserName = @UserName">
                <SelectParameters>
                    <asp:Parameter Name="UserName"/>
                </SelectParameters>
            </asp:SqlDataSource>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>


