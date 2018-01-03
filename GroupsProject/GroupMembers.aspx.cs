using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GroupMembers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int.Parse(Request.Params["gid"]);
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
        if(!bool.Parse(hidIsMod.Value))
            return;
        try
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();

            try
            {

                var checkBox = sender as CheckBox;
                bool ismod = checkBox.Checked;

                string gid = Server.UrlDecode(Request.Params["gid"]);
                // luam date despre grup din baza de date
                string query = "UPDATE GroupsLists SET IsModerator=@ismod " +
                               "WHERE GroupId = @gid AND UserName = @uname";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("gid", gid);
                cmd.Parameters.AddWithValue("uname", checkBox.Text);
                cmd.Parameters.AddWithValue("ismod", ismod);
                cmd.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                StatusMsg.Text += "\n--" + exception.Message;
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
    }

    protected void CBMemMod_OnCheckedChanged(object sender, EventArgs e)
    {
        if (!bool.Parse(hidIsMod.Value))
            return;
        try
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
            con.Open();

            try
            {
                var checkBox = sender as CheckBox;
                bool ismem = checkBox.Checked;

                string gid = Server.UrlDecode(Request.Params["gid"]);
                // luam date despre grup din baza de date
                string query = "UPDATE GroupsLists SET IsMember=@ismem " +
                                    "WHERE GroupId = @gid AND UserName = @uname";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("gid", gid);
                cmd.Parameters.AddWithValue("uname", checkBox.Text);
                cmd.Parameters.AddWithValue("ismem", ismem);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception exception)
            {
                StatusMsg.Text += "\n--" + exception.Message;
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
    }
}