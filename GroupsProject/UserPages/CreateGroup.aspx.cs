using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

public partial class CreateGroup : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void CategoryDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            var ddl = LoginView1.FindControl("CategoryDropDownList") as DropDownList;
            var l = LoginView1.FindControl("NewCategoryLabel") as Label;
            var nc = LoginView1.FindControl("NewCategory") as TextBox;
            var cl = LoginView1.FindControl("CategoryList") as DropDownList;
            if (ddl.SelectedValue == "1") {
                l.Visible = false;
                nc.Visible = false;
                cl.Visible = true;
            } else
            {
                l.Visible = true;
                nc.Visible = true;
                cl.Visible = false;
            }
        }
        
    }

    protected void CreateButton_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var dbe = LoginView1.FindControl("DBError") as Literal;
            try
            {
                var groupName = LoginView1.FindControl("GroupName") as TextBox;
                var groupDesc = LoginView1.FindControl("GroupDescription") as TextBox;
                var ddl = LoginView1.FindControl("CategoryDropDownList") as DropDownList;
                var cl = LoginView1.FindControl("CategoryList") as DropDownList;
                var categoryId = int.Parse(cl.SelectedValue);       // existing category
                
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
                con.Open();
                
                if (ddl.SelectedValue == "2")       // new category
                {
                    var nc = LoginView1.FindControl("NewCategory") as TextBox;
                    // check if nc's text is actually an existing category
                    // compare toUpper both and strip any spaces from nc.Text
                    // then insert into categories
                    
                    string newCat = nc.Text.Trim();
                    var ncc = newCat.ToUpper();
                    int n1, n2;
                    n1 = n2 = 0;
                    foreach (ListItem item in cl.Items)
                    {
                        ++n1;
                        if (item.Text.ToUpper() != ncc)
                        {
                            ++n2;
                        }
                        else
                        {
                            categoryId = int.Parse(item.Value);
                            break;
                        }
                    }
                    if (n1 == n2)
                    {
                        
                        try
                        {
                            string gQuery = "INSERT INTO Categories (CategoryName) VALUES (@name)";
                            SqlCommand c = new SqlCommand(gQuery, con);
                            c.Parameters.AddWithValue("name", newCat);
                            categoryId = (int) c.ExecuteScalar();

                        }
                        catch (Exception exception)
                        {
                            dbe.Text = exception.Message;
                            throw exception; // so we close the connection
                        }
                        
                    }
                }


                try
                {
                    string groupQuery = "INSERT INTO Groups (GroupName, CategoryId, GroupDescription)" +
                        "VALUES (@gName, @gCatId, @gDesc); SELECT CAST(scope_identity() AS int)";
                    SqlCommand cmd = new SqlCommand(groupQuery, con);
                    cmd.Parameters.AddWithValue("gName", groupName.Text);
                    cmd.Parameters.AddWithValue("gCatId", categoryId);
                    cmd.Parameters.AddWithValue("gDesc", groupDesc.Text);
                    int gid = (int) cmd.ExecuteScalar();
                    
                    dbe.Text = "New group created successfully!";

                    AddInitialMembers(dbe, con, gid);

                    dbe.Text += "\nYou're all set!";
                }
                catch (Exception exception)
                {
                    dbe.Text = "DB insert error" + exception.Message;
                }
                finally
                {
                    con.Close();
                }
                

            }
            catch (SqlException s)
            {
                dbe.Text = "Database connection error:" + s.Message;
            }
            catch (Exception ex)
            {
                dbe.Text = ex.Message + "2";
            }

            Page.Response.Redirect("~/UserPages/MyGroups.aspx");
        }
    }

    private void AddInitialMembers(Literal dbe, SqlConnection con, int gid)
    {
        try
        {
            // now add members
            var uname = User.Identity.Name;
            var adminsList = Roles.GetUsersInRole("Admin");

            string memberQuery = "INSERT INTO GroupsLists (UserName, GroupId, IsModerator, IsMember)" +
                "VALUES (@name, @gId, @isMod, @isMem)";
            
                SqlCommand cmd = new SqlCommand(memberQuery, con);
                cmd.Parameters.AddWithValue("name", uname);
                cmd.Parameters.AddWithValue("gId", gid);
                cmd.Parameters.AddWithValue("isMod", true);
                cmd.Parameters.AddWithValue("isMem", true);
                cmd.ExecuteNonQuery();
            if (User.IsInRole("Admin"))
            {
                // remove thyself
                adminsList = adminsList.Where(a => a != uname).ToArray();
            }
            foreach (var admin in adminsList)
            {
                SqlCommand cmd2 = new SqlCommand(memberQuery, con);
                cmd2.Parameters.AddWithValue("name", admin);
                cmd2.Parameters.AddWithValue("gId", gid);
                cmd2.Parameters.AddWithValue("isMod", false);
                cmd2.Parameters.AddWithValue("isMem", true);
                cmd2.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            dbe.Text += "\n-" + ex.Message;
            throw ex;
        }
    }
}