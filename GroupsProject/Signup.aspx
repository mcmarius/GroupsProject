<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Signup" %>



<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <style type="text/css">
        .cls { display: none; }
    </style>
    
    
    <asp:LoginView runat="server">
        <LoggedInTemplate>
            <% Response.Redirect("~/Index.aspx"); %>
        </LoggedInTemplate>
    </asp:LoginView>

    <br/>
    <h1>Sign up</h1>
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
        <CreateUserButtonStyle CssClass="cls"></CreateUserButtonStyle>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="UserNameLabel" runat="server" Text="User name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqValUser" runat="server" Display="Dynamic"
                                                            Text="Please enter your user name!"
                                                            ControlToValidate="UserName" ValidationGroup="signup"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="PasswordLabel" runat="server" Text="Password: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqValPass" runat="server" Display="Dynamic"
                                                            Text="Please enter a password!"
                                                            ControlToValidate="Password" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegExPass" runat="server" Display="Dynamic"
                                                                ErrorMessage="The password should have at least 10 characters, at least one digit and one alphabetic character, and must not contain special characters"
                                                                ControlToValidate="Password" ValidationGroup="signup"
                                                                ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{10,30})$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm password"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqValConfPass" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please confirm your password!"
                                                            ControlToValidate="ConfirmPassword" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CmpVal" runat="server" Display="Dynamic"
                                                      Text="Passwords don't match!"
                                                      ControlToValidate="ConfirmPassword"
                                                      ControlToCompare="Password"
                                                      Operator="Equal" ValidationGroup="signup"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="EmailLabel" runat="server" Text="Email: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqValEmail" runat="server" Display="Dynamic"
                                                            Text="Please enter an e-mail address!"
                                                            ControlToValidate="Email" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegExVal" runat="server" Display="Dynamic"
                                                                ErrorMessage="Invalid e-mail format!"
                                                                ControlToValidate="Email" ValidationGroup="signup"
                                                                ValidationExpression="^(?(&quot;&quot;)(&quot;&quot;.+?&quot;&quot;@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="True" Visible="True" Enabled="True" ValidationGroup="signup"/>--%>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <%-- todo validate stuff --%>
                    </table>
                        <asp:Button runat="server" ID="SignupButton" Text="Sign up!"
                                    OnClick="CreateUserWizard1_OnCreatedUser" ValidationGroup="signup"/>
                </ContentTemplate>
            </asp:CreateUserWizardStep>

            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server" Title="?">
                <ContentTemplate>
                    Success!
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>


