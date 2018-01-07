<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewPost.aspx.cs" Inherits="UserPages.NewPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Create a new post</h1>
    
    <%-- todo validations --%>
    <asp:Literal runat="server" ID="StatusMsg"></asp:Literal>
    <br/>
    <div>
        <asp:Literal runat="server" Text="Select post type: "></asp:Literal>
        <asp:DropDownList ID="PostTypeDDL" runat="server" AutoPostBack="True"
                          OnSelectedIndexChanged="PostTypeDDL_OnSelectedIndexChanged">
            <Items>
                <asp:ListItem Value="Message"></asp:ListItem>
                <asp:ListItem Value="Poll"></asp:ListItem>
                <asp:ListItem Value="File"></asp:ListItem>
            </Items>
        </asp:DropDownList>
    </div>
    <br/>
    <asp:Panel runat="server" ID="MessagePanel">
        <div>
            <asp:Label ID="MsgTitleL" runat="server" Text="Message title"></asp:Label>
            <asp:TextBox ID="MsgTitle" runat="server"></asp:TextBox>
        </div>
        <br/>
        <div>
            <asp:Label ID="MsgContentL" runat="server" Text="Message content"></asp:Label>
            <asp:TextBox ID="MsgContentTB" runat="server"></asp:TextBox>
        </div>
        <br/>
        
    </asp:Panel>
    <asp:Panel runat="server" Visible="False" ID="PollPanel">
        <div>
            <asp:Label ID="PollQL" runat="server" Text="Poll question"></asp:Label>
            <asp:TextBox ID="PollQTB" runat="server"></asp:TextBox>
        </div>
        <br/>
        <div>
            <asp:Label ID="PollL" runat="server" Text="Poll options"></asp:Label>
            <asp:DropDownList ID="PollTypeDDL" runat="server" AutoPostBack="True"
                              OnSelectedIndexChanged="PollTypeDDL_OnSelectedIndexChanged">
                <asp:ListItem Value="false" Text="Multiple choice"></asp:ListItem>
                <asp:ListItem Value="true" Text="Single choice"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <br/>
        <asp:Panel runat="server" ID="CPanel">
            <div>
                <asp:CheckBoxList ID="PollCBList" runat="server">
        
                </asp:CheckBoxList>
                <asp:Label ID="COptionLabel" runat="server" Text="Poll options"></asp:Label>
                <asp:TextBox ID="COptionTB" runat="server"></asp:TextBox>
                <asp:Button runat="server" ID="AddCBButton" Text="Add option" OnClick="AddCBButton_OnClick"/>
            </div>
        </asp:Panel>
        
        <asp:Panel runat="server" ID="RPanel" Visible="False">
            <div>
                <asp:RadioButtonList ID="PollRBList" runat="server">
            
                </asp:RadioButtonList>
                <asp:Label ID="ROptionLabel" runat="server" Text="Poll options"></asp:Label>
                <asp:TextBox ID="ROptionTB" runat="server"></asp:TextBox>
                <asp:Button runat="server" ID="AddRBButton" Text="Add option" OnClick="AddRBButton_OnClick"/>
            </div>
        </asp:Panel>
        <br/>
    </asp:Panel>
    <asp:Panel runat="server" Visible="False" ID="FilePanel">
        <div>
            <asp:Label ID="FileL" runat="server" Text="Upload a file..."></asp:Label>
            <asp:FileUpload ID="FileUpload" runat="server"/>
        </div>
        <br/>
    </asp:Panel>
    <asp:Button runat="server" ID="PostButton" Text="Create post"/>
</asp:Content>

