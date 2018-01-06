using System;
using System.Data.SqlClient;

public partial class GroupPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int gid2 = 0;
        try
        {
            gid2 = int.Parse(Server.UrlDecode(Request.Params["gid"]) ?? throw new InvalidOperationException());
        }
        catch (Exception exception)
        {
            Response.Redirect("~/UserPages/MyGroups.aspx");
            Console.WriteLine(exception.Message + "\n");
        }
        if (Request.Params["gid"] != null)
        {
            string gid = Server.UrlDecode(Request.Params["gid"]);
            int catId = 0;
            bool isMod = false;
            bool isMem = false;
            bool mem = false;
            
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
                con.Open();

                try
                {
                    // luam date despre grup din baza de date
                    string groupQuery = "SELECT [GroupName], [GroupDescription], [CategoryId] " +
                                        "FROM [Groups] WHERE GroupId = @gid";
                    SqlCommand cmd = new SqlCommand(groupQuery, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TitleLiteral.Text = reader["GroupName"].ToString();
                        GNameLiteral.Text = reader["GroupName"].ToString();
                        GDescLiteral.Text = reader["GroupDescription"].ToString();
                        catId = int.Parse(reader["CategoryId"].ToString());
                    }
                    reader.Close();
                    // afisam categoria
                    string cQuery = "SELECT [CategoryName] FROM [Categories] WHERE CategoryId = @catid";
                    SqlCommand cmd2 = new SqlCommand(cQuery, con);
                    cmd2.Parameters.AddWithValue("catid", catId);
                    reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        CategoryLiteral.Text = reader["CategoryName"].ToString();
                    }
                    reader.Close();
                    
                    // acum vedem daca userul este membru al grupului
                    string glist = "SELECT [IsModerator], [IsMember] FROM GroupsLists WHERE UserName = @uname AND GroupId = @gid";
                    SqlCommand cmd3 = new SqlCommand(glist, con);
                    cmd3.Parameters.AddWithValue("uname", User.Identity.Name);
                    cmd3.Parameters.AddWithValue("gid", gid);
                    reader = cmd3.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        //var modCB = LV.FindControl("ModCB") as CheckBox;
                        //var memberCB = LV.FindControl("MemberCB") as CheckBox;
                        //modCB.Checked = 
                        mem = true;
                            isMod = bool.Parse(reader["IsModerator"].ToString());
                        //memberCB.Checked = 
                            isMem = bool.Parse(reader["IsMember"].ToString());
                    }

                    if (isMem){ hidIsMem.Value = "true"; }
                    // else { hidIsMem.Value = "false"; }
                    if (isMod) { hidIsMod.Value = "true"; }
                    // else { hidIsMod.Value = "false"; }
                    if (mem) { hidMem.Value = "true"; }
                    

                    loadPosts(con, gid);
                    // apoi afisam postarile, fisierele, activitatile
                }
                catch (Exception exception)
                {
                    StatusMsg.Text += "\n" + exception.Message;
                }
                finally
                {
                    con.Close();
                }
                

            }
            catch (Exception ex)
            {
                StatusMsg.Text += "\n" + ex.Message;
            }
            
            HLMembers.NavigateUrl = "~/GroupMembers.aspx?gid=" + Server.UrlEncode(gid2.ToString());
        }
    }

    private void loadPosts(SqlConnection con, string gid)
    {
        //try
        //{
        //    string query = "SELECT [PostId], [PostType], [PostDate] WHERE [GroupId] = @gid";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("gid", gid);
            
        //    SqlDataReader reader = cmd.ExecuteReader();
        //}
        //catch (Exception e)
        //{
        //    StatusMsg.Text += "\n" + e.Message;
        //    throw e;
        //}
    }

    protected void LeaveButton_OnClick(object sender, EventArgs e)
    {
        string gid = Server.UrlDecode(Request.Params["gid"]);
        bool redirect = true;
        try
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();
            try
            {
                int count = 0;
                string queryMods = "SELECT COUNT(*) AS 'c' FROM GroupsLists WHERE GroupId = @gid AND IsModerator = 1";
                SqlCommand qcom = new SqlCommand(queryMods, con);
                qcom.Parameters.AddWithValue("gid", gid);
                SqlDataReader reader = qcom.ExecuteReader();
                while (reader.Read())
                {
                    count = int.Parse(reader["c"].ToString());
                }
                reader.Close();
                
                // now get user's rights
                bool isMod = false;
                string qUsr = "SELECT IsModerator FROM GroupsLists WHERE UserName = @uname AND GroupId = @gid";
                SqlCommand cmdU = new SqlCommand(qUsr, con);
                cmdU.Parameters.AddWithValue("uname", User.Identity.Name);
                cmdU.Parameters.AddWithValue("gid", gid);
                reader = cmdU.ExecuteReader();
                while (reader.Read())
                {
                    isMod = bool.Parse(reader["IsModerator"].ToString());
                }
                reader.Close();

                if (count > 1 || (count == 1 && !isMod))
                {
                    // remove thyself
                    string query = "DELETE FROM GroupsLists WHERE UserName = @uname AND GroupId = @gid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("uname", User.Identity.Name);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.ExecuteNonQuery();
                    Response.Redirect("~/UserPages/MyGroups.aspx");
                }
                else
                {
                    StatusMsg.Text = "You can't leave this group because you are the only moderator!";
                    redirect = false;
                }
            }
            catch (Exception exception)
            {
                StatusMsg.Text += "\n" + exception.Message;
            }
            finally
            {
                con.Close();
                if (redirect)
                {
                    Response.Redirect("GroupPage.aspx?gid=" + Server.UrlEncode(gid));
                }
            }
        }
        catch (Exception ex)
        {
            StatusMsg.Text += "\n" + ex.Message;
        }
    }

    protected void JoinButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            string gid = Server.UrlDecode(Request.Params["gid"]);
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();

            try
            {
                string query = "INSERT INTO GroupsLists (UserName, GroupId, IsModerator, IsMember)" +
                    "VALUES (@uname, @gid, @ismod, @ismem)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("uname", User.Identity.Name);
                cmd.Parameters.AddWithValue("gId", gid);
                cmd.Parameters.AddWithValue("isMod", false);
                cmd.Parameters.AddWithValue("isMem", false);
                cmd.ExecuteNonQuery();
                StatusMsg.Text = "You have applied for membership in this group!";
                Response.Redirect("~/UserPages/MyGroups.aspx");
            }
            catch (Exception ex)
            {
                StatusMsg.Text += "\n" + ex.Message;
            }
        }
        catch (Exception ex)
        {
            StatusMsg.Text += "\n" + ex.Message;
        }
    }

    protected void DelButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            string gid = Server.UrlDecode(Request.Params["gid"]);
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();
            
            try
            {
                string query = "DELETE FROM [Groups] WHERE GroupId = @gid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("gid", gid);
                cmd.ExecuteNonQuery();
                StatusMsg.Text = "You have deleted this group!";
                Response.Redirect("~/UserPages/MyGroups.aspx");
            }
            catch (Exception ex)
            {
                StatusMsg.Text += "\n" + ex.Message;
            }
        }
        catch (Exception ex)
        {
            StatusMsg.Text += "\n" + ex.Message;
        }
    }

    protected void MemButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/GroupMembers.aspx?gid=" + Server.UrlEncode(Request.Params["gid"]));
    }
}