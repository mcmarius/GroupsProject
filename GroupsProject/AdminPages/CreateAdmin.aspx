<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateAdmin.aspx.cs" Inherits="CreateAdmin" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:CreateUserWizard ID="CreateAdminWizard" runat="server"
                          OnCreatedUser="CreateUserWizard1_OnCreatedAdmin">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateAdminWizardStep1" runat="server">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server" ValidateRequestMode="Enabled">
                <ContentTemplate>
                    k thx bye
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>--%>
    <asp:LoginView runat="server" ID="LV">
        <AnonymousTemplate>
            <%Response.Redirect("~/Index.aspx"); %>
        </AnonymousTemplate>
        <RoleGroups>
            <asp:RoleGroup runat="server" Roles="User">
                <ContentTemplate><% Response.Redirect("~/Index.aspx"); %></ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
        <RoleGroups>
            <asp:RoleGroup  runat="server" Roles="Admin">
                <ContentTemplate>
                    <asp:CreateUserWizard ID="_CreateAdminWizard" runat="server"
                                          OnCreatedUser="CreateUserWizard1_OnCreatedAdmin">
                        <WizardSteps>
                            <asp:CreateUserWizardStep ID="_CreateAdminWizardStep1" runat="server">
                            </asp:CreateUserWizardStep>
                            <asp:CompleteWizardStep ID="_CompleteWizardStep1" runat="server" ValidateRequestMode="Enabled">
                                <ContentTemplate>
                                    k thx bye
                                </ContentTemplate>
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                    </asp:CreateUserWizard>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
</asp:Content>