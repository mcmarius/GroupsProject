<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Warnings.aspx.cs" Inherits="UserPages.Warnings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Literal runat="server" ID="StatusMsg"></asp:Literal>
    <asp:LoginView runat="server" ID="LV">
        <AnonymousTemplate>
            You are not logged in!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h2 style="color: red">Warnings</h2>
            <asp:Repeater runat="server" ID="WarnRepeat" DataSourceID="WarnSource1">
                <ItemTemplate>
                    <asp:Label runat="server" ID="GName"
                               Text='<%# DataBinder.Eval(Container.DataItem, "GroupName") %>'>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="WarnTB" Enabled="False"
                                 Text='<%# DataBinder.Eval(Container.DataItem, "WarningMessage") %>'>
                    </asp:TextBox>
                    <br/>
                    <br/>
                </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource runat="server" ID="WarnSource1" OnSelecting="WarnSource_OnSelecting"
                               ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                               SelectCommand="SELECT WarningMessage, GroupName
                                                FROM Warnings
                                                INNER JOIN Groups
                                                ON Warnings.GroupId = Groups.GroupId
                                                WHERE UserName = @uname">
                <SelectParameters>
                    <asp:Parameter Name="uname"/>
                </SelectParameters>
            </asp:SqlDataSource>
        </LoggedInTemplate>
        <RoleGroups>
            <asp:RoleGroup Roles="Admin">
                <ContentTemplate>
                    <h2 style="color: red">Warnings</h2>
                    <asp:Repeater runat="server" ID="WarnRepeat" DataSourceID="WarnSource2">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="GName"
                                       Text='<%# DataBinder.Eval(Container.DataItem, "GroupName") %>'>
                            </asp:Label>
                            <asp:TextBox runat="server" ID="WarnTB" Enabled="False" 
                                         Text='<%# DataBinder.Eval(Container.DataItem, "WarningMessage") %>'>
                            </asp:TextBox>
                            <asp:Button runat="server" ID="DelButton" Text="Delete warning"
                                        OnClick="DelButton_OnClick"
                                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "WarningId") %>'/>
                            <br/>
                            <br/>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:SqlDataSource runat="server" ID="WarnSource2"
                                       ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                                       SelectCommand="SELECT WarningMessage, GroupName, WarningId
                                                FROM Warnings
                                                INNER JOIN Groups
                                                ON Warnings.GroupId = Groups.GroupId">
                    </asp:SqlDataSource>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>

</asp:Content>

