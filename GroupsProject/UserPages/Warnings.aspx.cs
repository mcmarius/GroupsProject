using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace UserPages
{
    public partial class Warnings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void WarnSource_OnSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@uname"].Value = User.Identity.Name;
        }

        protected void DelButton_OnClick(object sender, EventArgs e)
        {
            if (User.IsInRole("Admin"))
            {
                var button = sender as Button;
                try
                {
                    SqlConnection con =
                        new SqlConnection(
                            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
                    con.Open();

                    try
                    {
                        string query = "DELETE FROM Warnings WHERE WarningId = @wid";
                        SqlCommand cmd = new SqlCommand(query, con);
                        Debug.Assert(button != null, nameof(button) + " != null");
                        cmd.Parameters.AddWithValue("wid", button.ToolTip);
                        cmd.ExecuteNonQuery();
                        Response.Redirect("~/UserPages/Warnings.aspx");
                    }
                    catch (Exception exception)
                    {
                        StatusMsg.Text += exception.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                catch (Exception exception)
                {
                    StatusMsg.Text += exception.Message;
                }
            }
        }
    }
}