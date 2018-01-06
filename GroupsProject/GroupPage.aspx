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
    
    <asp:HiddenField runat="server" ID="hidMem" Value="false"/>
    <asp:HiddenField runat="server" ID="hidIsMem" Value="false"/>
    <asp:HiddenField runat="server" ID="hidIsMod" Value="false"/>
    
    
    <asp:LoginView runat="server" ID="LV">
        <AnonymousTemplate>
            
        </AnonymousTemplate>
        <LoggedInTemplate>
            <%-- view group details: name, description, category
                posts
                activities
                files --%>
            <br/>
            
            <% if (bool.Parse(hidMem.Value) && !User.IsInRole("Admin"))
               { %>
                <asp:Button runat="server" ID="LeaveButton" Text="Leave group" OnClick="LeaveButton_OnClick"/>
                
                <%-- display posts --%>
                
                
            <% }
               else if(!User.IsInRole("Admin"))
               { %>
                <asp:Button runat="server" ID="JoinButton" Text="Join group" OnClick="JoinButton_OnClick"/>
            <% } %>
            <%--<asp:CheckBox runat="server" ID="MemberCB" Enabled="False"/>
            <asp:CheckBox runat="server" ID="ModCB" Enabled="False"/>--%>
            <br/>
            <br/>
            
            <%-- navigation: join if pending or oth
                /leave if member
                members
                
                if isModerator ... dar asta la pagina cu membri--%>
            
            
        </LoggedInTemplate>
    </asp:LoginView>
            <%--<asp:HyperLink runat="server" ID="HLMembers">Members</asp:HyperLink>--%>
            <asp:Button runat="server" ID="MemButton" Text="Members" OnClick="MemButton_OnClick"/>
    <asp:LoginView runat="server" ID="LV2">
        <RoleGroups>
            <asp:RoleGroup Roles="Admin">
                <ContentTemplate>
                    <br/>
                    <div>
                        <%--<asp:HyperLink runat="server" ID="DelGroup"
                                       NavigateUrl='<%# "~/AdminPages/Confirm.aspx?gid=" + Server.UrlEncode(Request.Params["gid"].ToString()) %>' >Delete Group</asp:HyperLink>--%>
                        <asp:Button runat="server" ID="DelButton" Text="Delete group" OnClick="DelButton_OnClick"/>
                    </div>
                    
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
        
    </asp:LoginView>
</asp:Content>


