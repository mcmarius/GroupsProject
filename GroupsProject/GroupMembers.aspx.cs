﻿using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class GroupMembers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var unused = int.Parse(Server.UrlDecode(Request.Params["gid"]) ?? throw new InvalidOperationException());
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
                hidGName.Value = GNameLit.Text = reader["GroupName"].ToString();
            }
            reader.Close();
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

        if (!bool.Parse(hidIsMod.Value) || admins.Contains(checkBox.Text))
        {
            //💩
            Response.Redirect("GroupPage.aspx?gid=" + Server.UrlEncode(gid));
            return;
        }*/
        try
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();

            try
            {


                // luam date despre grup din baza de date
                
                string query;
                if (ismod)  // if you become mod, you should also be member
                {
                    query = "UPDATE GroupsLists SET IsModerator=@ismod, IsMember=@ismem " +
                            "WHERE GroupId = @gid AND UserName = @uname";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.Parameters.AddWithValue("uname", checkBox.Text);
                    cmd.Parameters.AddWithValue("ismod", true);
                    cmd.Parameters.AddWithValue("ismem", true);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE GroupsLists SET IsModerator=@ismod " +
                            "WHERE GroupId = @gid AND UserName = @uname";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("gid", gid);
                    Debug.Assert(checkBox != null, nameof(checkBox) + " != null");
                    cmd.Parameters.AddWithValue("uname", checkBox.Text);
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
        if (!bool.Parse(hidIsMod.Value) || admins.Contains(checkBox.Text))
        {
            Response.Redirect("GroupPage.aspx?gid=" + Server.UrlEncode(gid));
            return;
        }
        try
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();

            try
            {

                // luam date despre grup din baza de date
                if (ismem)
                {
                    string query = "UPDATE GroupsLists SET IsMember=@ismem " +
                                   "WHERE GroupId = @gid AND UserName = @uname";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.Parameters.AddWithValue("uname", checkBox.Text);
                    cmd.Parameters.AddWithValue("ismem", true);
                    cmd.ExecuteNonQuery();
                }
                else
                {       // if no longer member, you shouldn't be mod
                    string query = "UPDATE GroupsLists SET IsMember=@ismem, IsModerator=@ismod " +
                                   "WHERE GroupId = @gid AND UserName = @uname";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.Parameters.AddWithValue("uname", checkBox.Text);
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
                Response.Redirect("GroupPage.aspx?gid="+Server.UrlEncode(gid));
            }
        }
        catch (Exception ex)
        {
            StatusMsg.Text += "\n" + ex.Message;
        }
    }
}