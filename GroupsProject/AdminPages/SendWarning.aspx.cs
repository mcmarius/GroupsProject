using System;
using System.Data.SqlClient;

namespace AdminPages
{
    public partial class SendWarning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int unused = int.Parse(Server.UrlDecode(Request.Params["gid"]) ?? throw new InvalidOperationException());
            }
            catch (Exception exception)
            {
                StatusMsg.Text += exception.Message;
                Response.Redirect("~/UserPages/Warnings.aspx");
            }
        }

        protected void WarnButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                string gid = Server.UrlDecode(Request.Params["gid"]);
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
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

                    string modQuery = "SELECT UserName FROM GroupsLists WHERE GroupId = @gid AND IsModerator = 1";
                    SqlCommand mc = new SqlCommand(modQuery, con);
                    mc.Parameters.AddWithValue("gid", gid);
                    reader = mc.ExecuteReader();
                    int k = 0;
                    var s = new string[count];
                    while (reader.Read())
                    {
                        s[k++] = reader["UserName"].ToString();
                    }
                    reader.Close();

                    foreach (var mod in s)
                    {
                        string query = "INSERT INTO Warnings (WarningMessage, UserName, GroupId) " +
                                       "VALUES (@msg, @uname, @gid)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("msg", WarnMessage.Text);
                        cmd.Parameters.AddWithValue("uname", mod);
                        cmd.Parameters.AddWithValue("gid", gid);

                        cmd.ExecuteNonQuery();
                    }
                
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
}