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
        if (Request.Params["gid"] != null)
        {
            string gid = Server.UrlDecode(Request.Params["gid"]);
            int catId = 0;
            
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

                    // acum vedem daca userul este membru al grupului
                    string glist = "SELECT [UserName], [GroupId], [IsModerator], [IsMember] FROM GroupLists WHERE UserName=@uname";
                    
                    
                    
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
}