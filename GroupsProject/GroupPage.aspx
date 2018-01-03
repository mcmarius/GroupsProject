<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="GroupPage.aspx.cs" Inherits="GroupPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>
        <asp:Literal runat="server" ID="TitleLiteral" Text="My groups"></asp:Literal>
    </title>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal runat="server" ID="StatusMsg"></asp:Literal>
    <br/>
    <br/>
    
    <asp:Label runat="server" Text="Name: "></asp:Label>
    <asp:Literal runat="server" ID="GNameLiteral"></asp:Literal>
    <br/>
    <asp:Label runat="server" Text="Description: "></asp:Label>
    <asp:Literal runat="server" ID="GDescLiteral"></asp:Literal>
    <br/>
    <asp:Label runat="server" Text="Category: "></asp:Label>
    <asp:Literal runat="server" ID="CategoryLiteral"></asp:Literal>
    <br/>
    
    <asp:LoginView runat="server" ID="LV">
        <AnonymousTemplate>
            
        </AnonymousTemplate>
        <LoggedInTemplate>
            <%-- view group details: name, description, category
                posts
                activities
                files --%>
            
            
            
            <%-- navigation: join if pending or oth
                /leave if member
                members
                
                if isModerator ... dar asta la pagina cu membri--%>
            
            <% %>
            
        </LoggedInTemplate>
        <RoleGroups>
            <asp:RoleGroup runat="server" Roles="Admin">
                <ContentTemplate>
                    <asp:HyperLink runat="server" ID="DelGroup"
                                    NavigateUrl='<%# "~/AdminPages/Confirm.aspx?gid=" +
                                                     Server.UrlEncode(Request.Params["gid"])
                                                 %>'></asp:HyperLink>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
</asp:Content>


