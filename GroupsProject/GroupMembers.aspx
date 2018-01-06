<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GroupMembers.aspx.cs" Inherits="GroupMembers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <h2>Members</h2>
    <asp:Literal runat="server" ID="GNameLit"></asp:Literal>
    <%--<asp:GridView runat="server" ID="GV" DataSourceID="GMem"></asp:GridView>--%>
    <asp:Literal runat="server" ID="StatusMsg"></asp:Literal>
    <br/>
    <br/>
    
    <asp:HiddenField runat="server" ID="hidIsMem" Value="false"/>
    <asp:HiddenField runat="server" ID="hidIsMod" Value="false"/>
    <asp:HiddenField runat="server" ID="hidGName" Value="a"/>
    
    <asp:Repeater runat="server" ID="MyRepeater" DataSourceID="GMem">
        <ItemTemplate>
            <div style="padding: 10px">
                <h3> <%# DataBinder.Eval(Container.DataItem, "UserName") %> </h3>
                
                <asp:Label runat="server" ID="uname" Text='<%# DataBinder.Eval(Container.DataItem, "UserName") %>'></asp:Label>
                
                <% if (bool.Parse(hidIsMod.Value))
                   { %>
                    <div>
                        Member status: <asp:CheckBox runat="server" AutoPostBack="True" Enabled="True"
                                                     OnCheckedChanged="CBMemMod_OnCheckedChanged"
                                                     ID="CBMemMod"
                                                     Checked='<%# DataBinder.Eval(Container.DataItem, "IsMember") %>'
                                                     ToolTip='<%# DataBinder.Eval(Container.DataItem, "UserName") %>'/>
                    </div><div>
                    Mod status: <asp:CheckBox runat="server" AutoPostBack="True" Enabled="True"
                                              OnCheckedChanged="CBModMod_OnCheckedChanged"
                                              ID="CBModMod"
                                              Checked='<%# DataBinder.Eval(Container.DataItem, "IsModerator") %>'
                                              ToolTip='<%# DataBinder.Eval(Container.DataItem, "UserName") %>'/>
                </div>
                <% }
                   else
                   { %>
                    <div>
                        Member status: <asp:CheckBox runat="server" Enabled="False" ID="CBMem"
                             Checked='<%# DataBinder.Eval(Container.DataItem, "IsMember") %>'/>
                </div><div>
                    Mod status: <asp:CheckBox runat="server" Enabled="False" ID="CBMod"
                             Checked='<%# DataBinder.Eval(Container.DataItem, "IsModerator") %>'/>
                </div>
                    <% } %>

            </div>
        </ItemTemplate>

    </asp:Repeater>
    
    <asp:SqlDataSource runat="server" ID="GMem" ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                       SelectCommand="SELECT [UserName],[IsModerator],[IsMember] FROM GroupsLists WHERE GroupId=@gid" OnSelecting="GMem_OnSelecting">
        <SelectParameters>
            <asp:Parameter Name="gid"/>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

