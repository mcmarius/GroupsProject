using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class GroupMembers : System.Web.UI.Page
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
            Console.WriteLine(exception);
            Response.Redirect("~/Index.aspx");
        }

        string gid = Server.UrlDecode(Request.Params["gid"]);
        bool isMod = false;
        bool isMem = false;

        try
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();

            try
            {
                // acum vedem daca userul este membru al grupului
                string glist =
                    "SELECT [UserName], [IsModerator], [IsMember] FROM GroupsLists WHERE UserName = @uname AND GroupId = @gid";
                SqlCommand cmd3 = new SqlCommand(glist, con);
                cmd3.Parameters.AddWithValue("uname", User.Identity.Name);
                cmd3.Parameters.AddWithValue("gid", gid);
                SqlDataReader reader = cmd3.ExecuteReader();

                while (reader.Read())
                {
                    //var modCB = LV.FindControl("ModCB") as CheckBox;
                    //var memberCB = LV.FindControl("MemberCB") as CheckBox;
                    //modCB.Checked = 
                    isMod = bool.Parse(reader["IsModerator"].ToString());
                    //memberCB.Checked = 
                    isMem = bool.Parse(reader["IsMember"].ToString());
                }

                if (isMem)
                {
                    hidIsMem.Value = "true";
                }
                else
                {
                    hidIsMem.Value = "false";
                }
                if (isMod)
                {
                    hidIsMod.Value = "true";
                }
                else
                {
                    hidIsMod.Value = "false";
                }

                reader.Close();
                string groupQuery = "SELECT [GroupName] " +
                                    "FROM [Groups] WHERE GroupId = @gid";
                SqlCommand cmd = new SqlCommand(groupQuery, con);
                cmd.Parameters.AddWithValue("gid", gid);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hidGName.Value = reader["GroupName"].ToString();
                }
                reader.Close();

                GNameLink.NavigateUrl = "~/GroupPage.aspx?gid=" + Server.UrlEncode(gid2.ToString());
            }
            catch (Exception ex)
            {
                StatusMsg.Text += ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {
            StatusMsg.Text += ex.Message;
        }
    }

    protected void GMem_OnSelecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@gid"].Value = Server.UrlDecode(Request.Params["gid"]);
    }

    protected void CBModMod_OnCheckedChanged(object sender, EventArgs e)
    {
        string gid = Server.UrlDecode(Request.Params["gid"]);
        var checkBox = sender as CheckBox;
        Debug.Assert(checkBox != null, nameof(checkBox) + " != null");
        bool ismod = checkBox.Checked;
        /*var admins = Roles.GetUsersInRole("Admin");

        if (!bool.Parse(hidIsMod.Value) || admins.Contains(checkBox.ToolTip))
        {
            //💩
            Response.Redirect("GroupPage.aspx?gid=" + Server.UrlEncode(gid));
            return;
        }*/
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

                // luam date despre grup din baza de date

                string query;
                if (ismod) // if you become mod, you should also be member
                {
                    query = "UPDATE GroupsLists SET IsModerator=@ismod, IsMember=@ismem " +
                            "WHERE GroupId = @gid AND UserName = @uname";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.Parameters.AddWithValue("uname", checkBox.ToolTip);
                    cmd.Parameters.AddWithValue("ismod", true);
                    cmd.Parameters.AddWithValue("ismem", true);
                    cmd.ExecuteNonQuery();
                }
                if (count > 1 && !ismod)
                {
                    query = "UPDATE GroupsLists SET IsModerator=@ismod " +
                            "WHERE GroupId = @gid AND UserName = @uname";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    Debug.Assert(checkBox != null, nameof(checkBox) + " != null");
                    cmd.Parameters.AddWithValue("uname", checkBox.ToolTip);
                    cmd.Parameters.AddWithValue("ismod", false);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                StatusMsg.Text += "\n--" + exception.Message;
            }
            finally
            {
                con.Close();
                Response.Redirect("GroupPage.aspx?gid=" + Server.UrlEncode(gid));
            }
        }
        catch (Exception ex)
        {
            StatusMsg.Text += "\n" + ex.Message;
        }
    }

    protected void CBMemMod_OnCheckedChanged(object sender, EventArgs e)
    {
        string gid = Server.UrlDecode(Request.Params["gid"]);
        var checkBox = sender as CheckBox;
        Debug.Assert(checkBox != null, nameof(checkBox) + " != null");
        bool ismem = checkBox.Checked;
        var admins = Roles.GetUsersInRole("Admin");
        if (!bool.Parse(hidIsMod.Value) || admins.Contains(checkBox.ToolTip))
        {
            Response.Redirect("GroupPage.aspx?gid=" + Server.UrlEncode(gid));
            return;
        }
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
                cmdU.Parameters.AddWithValue("uname", checkBox.ToolTip);
                cmdU.Parameters.AddWithValue("gid", gid);
                reader = cmdU.ExecuteReader();
                while (reader.Read())
                {
                    isMod = bool.Parse(reader["IsModerator"].ToString());
                }
                reader.Close();


                // luam date despre grup din baza de date
                if (ismem)
                {
                    string query = "UPDATE GroupsLists SET IsMember=@ismem " +
                                   "WHERE GroupId = @gid AND UserName = @uname";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.Parameters.AddWithValue("uname", checkBox.ToolTip);
                    cmd.Parameters.AddWithValue("ismem", true);
                    cmd.ExecuteNonQuery();
                }
                else if (count > 1 || (count == 1 && !isMod))
                {
                    // if no longer member, you shouldn't be mod
                    string query = "UPDATE GroupsLists SET IsMember=@ismem, IsModerator=@ismod " +
                                   "WHERE GroupId = @gid AND UserName = @uname";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.Parameters.AddWithValue("uname", checkBox.ToolTip);
                    cmd.Parameters.AddWithValue("ismem", false);
                    cmd.Parameters.AddWithValue("ismod", false);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                StatusMsg.Text += "\n--" + exception.Message;
            }
            finally
            {
                con.Close();
                Response.Redirect("GroupPage.aspx?gid=" + Server.UrlEncode(gid));
            }
        }
        catch (Exception ex)
        {
            StatusMsg.Text += "\n" + ex.Message;
        }
    }

    protected void KickButton_OnClick(object sender, EventArgs e)
    {
        var button = sender as Button;
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
                Debug.Assert(button != null, nameof(button) + " != null");
                cmdU.Parameters.AddWithValue("uname", button.ToolTip);
                cmdU.Parameters.AddWithValue("gid", gid);
                reader = cmdU.ExecuteReader();
                while (reader.Read())
                {
                    isMod = bool.Parse(reader["IsModerator"].ToString());
                }
                reader.Close();

                if (count > 1 || (count == 1 && !isMod))
                {
                    // remove this user
                    string query = "DELETE FROM GroupsLists WHERE UserName = @uname AND GroupId = @gid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("uname", button.ToolTip);
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

    protected void KickButton_OnPreRender(object sender, EventArgs e)
    {
        var button = sender as Button;
        Debug.Assert(button != null, nameof(button) + " != null");
        if (button.ToolTip == User.Identity.Name)
        {
            button.Visible = button.Enabled = false;
        }

        var admins = Roles.GetUsersInRole("Admin");
        if (!bool.Parse(hidIsMod.Value) || admins.Contains(button.ToolTip))
        {
            button.Enabled = button.Visible = false;
        }
    }
}