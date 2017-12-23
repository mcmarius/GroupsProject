﻿<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="CreateAdmin.aspx.cs" Inherits="CreateAdmin" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:CreateUserWizard ID="CreateAdminWizard" runat="server"
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
    </asp:CreateUserWizard>

</asp:Content>