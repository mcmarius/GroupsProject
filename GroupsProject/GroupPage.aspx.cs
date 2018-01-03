using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GroupPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int.Parse(Request.Params["gid"]);
        }
        catch (Exception exception)
        {
            Response.Redirect("~/UserPages/MyGroups.aspx");
            Console.WriteLine(exception.Message);
        }
        if (Request.Params["gid"] != null)
        {
            string gid = Server.UrlDecode(Request.Params["gid"]);
            int catId = 0;
            bool isMod = false;
            bool isMem = false;
            
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
                            isMod = bool.Parse(reader["IsModerator"].ToString());
                        //memberCB.Checked = 
                            isMem = bool.Parse(reader["IsMember"].ToString());
                    }

                    if (isMem){ hidIsMem.Value = "true"; }
                    else { hidIsMem.Value = "false"; }
                    if (isMod) { hidIsMod.Value = "true"; }
                    else { hidIsMod.Value = "false"; }

                    //loadPosts(con, gid);
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
        }
    }

    protected void LeaveButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void JoinButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void DelButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            string gid = Request.Params["gid"];
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
}