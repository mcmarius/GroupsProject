﻿<%@ Page Title="Create a group" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateGroup.aspx.cs" Inherits="UserPages.CreateGroup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:LoginView runat="server" ID="LoginView1">
        <AnonymousTemplate>
            You are not logged in!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h2>Create a new group</h2>
            <!-- todo validation -->
            <asp:Literal runat="server" ID="DBError"></asp:Literal>
            <br/>
            <asp:Label runat="server" ID="GroupNameLabel" Text="Group name"></asp:Label>
            <br/>
            <asp:TextBox runat="server" ID="GroupName"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqValGName" runat="server" Display="Dynamic"
                                        ErrorMessage="Please enter the name of the new group!"
                                        ValidationGroup="gcreate" ControlToValidate="GroupName"></asp:RequiredFieldValidator>
            <br/>
            <br/>
            
            <asp:DropDownList ID="CategoryDropDownList" runat="server" AutoPostBack="True"
                              OnSelectedIndexChanged="CategoryDropDownList_OnSelectedIndexChanged"
                              ViewStateMode="Enabled" EnableViewState="True">
                <asp:ListItem Text="Existing category" Value="1"></asp:ListItem>
                <asp:ListItem Text="New category" Value="2"></asp:ListItem>
            </asp:DropDownList>
            
            <br/>
            <asp:DropDownList runat="server" ID="CategoryList" DataSourceID="SqlDataSource" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSource" ConnectionString="<%$ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [CategoryName], [CategoryId] FROM [Categories] ORDER BY [CategoryId]"></asp:SqlDataSource>
            
            <asp:Label runat="server" ID="NewCategoryLabel" Text="New category name" Visible="False"></asp:Label>
            <br/>
            <asp:TextBox runat="server" ID="NewCategory" Visible="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqValNewCat" runat="server" Display="Dynamic" Enabled="False"
                                        ErrorMessage="New category name cannot be empty!"
                                        ControlToValidate="NewCategory" ValidationGroup="gcreate"></asp:RequiredFieldValidator>
            <br/>
            
            <asp:Label runat="server" ID="GroupDescriptionLabel" Text="Group description"></asp:Label>
            <br/>
            <asp:TextBox runat="server" ID="GroupDescription"></asp:TextBox>
            <br/>
            <asp:RequiredFieldValidator ID="ReqValDesc" runat="server" Display="Dynamic"
                                        ErrorMessage="Please tell users what this group ia about!"
                                        ControlToValidate="GroupDescription" ValidationGroup="gcreate"></asp:RequiredFieldValidator>
            <br/>
            
            <asp:Button ID="CreateButton" runat="server" Text="Create!" CausesValidation="True" ValidateRequestMode="Enabled" OnClick="CreateButton_OnClick" ValidationGroup="gcreate"/>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

