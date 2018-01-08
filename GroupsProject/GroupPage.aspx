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
            
            <% if (bool.Parse(hidMem.Value))
               {
                   if (!User.IsInRole("Admin"))
                   { %>
                    <asp:Button runat="server" ID="LeaveButton" Text="Leave group" OnClick="LeaveButton_OnClick"/>
                <% } %>
                
            <% }%>
            
            <%
               else if(!User.IsInRole("Admin"))
               { %>
                <asp:Button runat="server" ID="JoinButton" Text="Join group" OnClick="JoinButton_OnClick"/>
            <% } %>
            <%--<asp:CheckBox runat="server" ID="MemberCB" Enabled="False"/>
            <asp:CheckBox runat="server" ID="ModCB" Enabled="False"/>--%>
            <br/>
            <% if(bool.Parse(hidIsMem.Value)){ %>
                <h3>Posts</h3>
                
                <asp:Repeater runat="server" ID="PostRepeater" DataSourceID="PostDS">
                    <ItemTemplate>
                        
                        <%-- post date --%>
                        Posted by 
                        <asp:Literal runat="server" ID="PostAuthorLit"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "Author") %>'></asp:Literal>
                        on
                        <asp:Literal runat="server" ID="PostLit"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "PostDate") %>'></asp:Literal>
                        <br/>

                        <%-- post content --%>
                        <asp:Panel runat="server" ID="MessagePanel"
                                   Visible='<%#DataBinder.Eval(Container.DataItem, "PostType").ToString() == "message" %>'>
                            
                            <div>
                                Title:
                                <asp:Literal runat="server" ID="MsgLit" Text='<%#DataBinder.Eval(Container.DataItem, "MessageTitle") %>'></asp:Literal>
                            </div>
                            <br/>
                            <div>
                                Content:
                                <asp:Literal runat="server" ID="Literal2" Text='<%#DataBinder.Eval(Container.DataItem, "MessageContent") %>'></asp:Literal>
                            </div>
                        </asp:Panel>
                        <%-- else, if it's a poll --%>
                        <asp:Panel runat="server" ID="PollPanel"
                                   Visible='<%#DataBinder.Eval(Container.DataItem, "PostType").ToString() == "poll"%>'>
                            Question:
                            <asp:Literal runat="server" ID="PollLitM"
                                         Text='<%#DataBinder.Eval(Container.DataItem, "PollQuestion")%>'>
                            </asp:Literal>
                            <br/>
                            Options:
                            <asp:Panel runat="server" ID="CBList" Visible='<%#DataBinder.Eval(Container.DataItem, "PollType") is bool  ?
                                                                                  !(bool) DataBinder.Eval(Container.DataItem, "PollType") : false%>'>
                                <asp:CheckBoxList runat="server" ID="Mlist" DataSourceID="MChoice" OnUnload="Mlist_OnUnload"
                                                  DataValueField="OptionId" DataTextField="Display"
                                                   AutoPostBack="True" ToolTip="PollId"
                                                  Enabled='<%#DataBinder.Eval(Container.DataItem, "Ans") is bool ? 
                                                                  !(bool) DataBinder.Eval(Container.DataItem, "Ans")  : false %>'/>
                                
                                
                                <asp:SqlDataSource runat="server" ID="MChoice" OnSelecting="MChoice_OnSelecting"
                                                   ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                                                   SelectCommand="SELECT OptionId, OptionName, OptionCount, COALESCE (OptionName + ' (' + STR(OptionCount) + ' )' ,'') AS 'Display'
                                    FROM Options WHERE PollId=@pollId">
                                    <SelectParameters><asp:Parameter Name="pollId"/></SelectParameters>
                                </asp:SqlDataSource>
                                
                                <asp:Button runat="server" ID="SubmitPollButton" Text="Answer question"
                                            OnClick="SubmitPollButton_OnClick"
                                            Enabled='<%#DataBinder.Eval(Container.DataItem, "Ans") is bool ? !(bool) DataBinder.Eval(Container.DataItem, "Ans")  : false %>'/>
                            </asp:Panel>
                            
                            <asp:Panel runat="server" ID="RBList" Visible='<%#DataBinder.Eval(Container.DataItem, "PollType") is bool ? (bool) DataBinder.Eval(Container.DataItem, "PollType") : false %>'>
                                
                            </asp:Panel>
                        </asp:Panel>
                                                                              <%-- else, if it's a file --%>
                        <asp:Panel runat="server" ID="FilePanel"
                                   Visible='<%#DataBinder.Eval(Container.DataItem, "PostType").ToString() == "file" %>'>
                            
                            <asp:Literal runat="server" ID="Literal1"
                                         Text='<%#DataBinder.Eval(Container.DataItem, "FileName")%>'>
                            </asp:Literal>
                        </asp:Panel>
                        <br/>
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:SqlDataSource runat="server" ID="PostDS" OnSelecting="PostDS_OnSelecting"
                                   ConnectionString="<%$ConnectionStrings:ConnectionString %>"
                                   SelectCommand="SELECT PostDate, PostType, Posts.UserName AS 'Author',
                            MessageTitle, MessageContent,
                            PollType, PollQuestion, Polls.PollId, 
							PollsAnswers.Answered AS 'Ans', PollsAnswers.UserName as 'UPoll',
                            FileName, FileContent, FileId
                            FROM Posts
                            FULL OUTER JOIN Polls ON Posts.PostId = Polls.PostId
                            FULL OUTER JOIN Messages ON Posts.PostId = Messages.PostId
                            FULL OUTER JOIN Files ON Posts.PostId = Files.PostId
							FULL OUTER JOIN PollsAnswers ON Polls.PollId = PollsAnswers.PollId
                            WHERE GroupId = @gid
                            AND PollsAnswers.UserName = @uname
                            ORDER BY PostDate DESC;">
                                   
                                   
                                   <%-- "SELECT PostDate, PostType, Posts.UserName AS 'Author',
                            MessageTitle, MessageContent,
                            PollType, PollQuestion, PollId, PollAnswered, Polls.UserNAme as 'UPoll',
                            FileName, FileContent, FileId
                            FROM Posts
                            FULL OUTER JOIN Polls ON Posts.PostId = Polls.PostId
                            FULL OUTER JOIN Messages ON Posts.PostId = Messages.PostId
                            FULL OUTER JOIN Files ON Posts.PostId = Files.PostId
                            WHERE GroupId = @gid
                            ORDER BY PostDate DESC;">
                    <%-- SELECT PostDate, PostType,
                            IsNull(MessageTitle, '') AS 'MessageTitle', IsNull(MessageContent, '') AS 'MessageContent',
                            IsNull(PollType, '') AS 'PollType', IsNull(PollQuestion, '') AS 'PollQuestion',
                            IsNull(FileName, ''), IsNull(FileContent, -1)
                            FROM Posts
                            FULL OUTER JOIN Polls ON Posts.PostId = Polls.PostId
                            FULL OUTER JOIN Messages ON Posts.PostId = Messages.PostId
                            FULL OUTER JOIN Files ON Posts.PostId = Files.PostId --%>
                    <SelectParameters>
                        <asp:Parameter Name="gid"/>
                        <asp:Parameter Name="uname"/>
                    </SelectParameters>
                </asp:SqlDataSource>
                
                <asp:Button runat="server" ID="NewPost" Text="Create new post" OnClick="NewPost_OnClick"/>
                
            <% }%>
            <br/>
            
            <%-- navigation: join if pending or oth
                /leave if member
                members
                
                if isModerator ... dar asta la pagina cu membri--%>
            
            
        </LoggedInTemplate>
    </asp:LoginView>
    <br/>
            <asp:HyperLink runat="server" ID="HLMembers">Members</asp:HyperLink>
            <%--<asp:Button runat="server" ID="MemButton" Text="Members" OnClick="MemButton_OnClick"/>--%>
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
                    <div>
                        <asp:Button runat="server" ID="WarnButton" Text="Send warning!" OnClick="WarnButton_OnClick"/>
                        <%-- on warnings page, remove warnings --%>
                    </div>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
        
    </asp:LoginView>
</asp:Content>


