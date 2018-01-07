using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace UserPages
{
    public partial class NewPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Index.aspx");
            }
            // only continue if the user belongs to the group
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Groups.mdf;Integrated Security=True;User Instance=False");
                con.Open();
                bool isMem = false;
                string gid = Server.UrlDecode(Request.Params["gid"]);
                try
                {
                    string glist = "SELECT [IsModerator], [IsMember] FROM GroupsLists WHERE UserName = @uname AND GroupId = @gid";
                    SqlCommand cmd3 = new SqlCommand(glist, con);
                    cmd3.Parameters.AddWithValue("uname", User.Identity.Name);
                    cmd3.Parameters.AddWithValue("gid", gid);
                    SqlDataReader reader = cmd3.ExecuteReader();

                    while (reader.Read())
                    {
                        isMem = bool.Parse(reader["IsMember"].ToString());
                    }
                    reader.Close();

                    if (!isMem) { Response.Redirect("~/Index.aspx"); }
                
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

        protected void PostTypeDDL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (PostTypeDDL.SelectedValue == "Message")
                {
                    MessagePanel.Visible = true;
                    PollPanel.Visible = FilePanel.Visible = false;
                    //MsgTitleL.Visible = MsgTitle.Visible = MsgContentL.Visible = MsgContentTB.Visible = true;
                    //PollQL.Visible = PollQTB.Visible = PollL.Visible = PollTypeDDL.Visible = PollCBList.Visible = COptionLabel.Visible
                    //  = COptionTB.Visible = AddCBButton.Visible = PollRBList.Visible = ROptionLabel.Visible
                    //= ROptionTB.Visible = AddRBButton.Visible = FileL.Visible = FileUpload.Visible = false;
                }
                else if (PostTypeDDL.SelectedValue == "Poll")
                {
                    MessagePanel.Visible = FilePanel.Visible = false;
                    PollPanel.Visible = true;
                    /*MsgTitleL.Visible = MsgTitle.Visible = MsgContentL.Visible = MsgContentTB.Visible = false;
                    PollQL.Visible = PollQTB.Visible = PollL.Visible = PollTypeDDL.Visible = PollCBList.Visible =
                        COptionLabel.Visible
                            = COptionTB.Visible = AddCBButton.Visible = PollRBList.Visible = ROptionLabel.Visible
                                = ROptionTB.Visible = AddRBButton.Visible = true;
                    FileL.Visible = FileUpload.Visible = false;*/
                }
                else
                {
                    MessagePanel.Visible = PollPanel.Visible = false;
                    FilePanel.Visible = true;
                    /*MsgTitleL.Visible = MsgTitle.Visible = MsgContentL.Visible = MsgContentTB.Visible = false;
                    PollQL.Visible = PollQTB.Visible = PollL.Visible = PollTypeDDL.Visible = PollCBList.Visible =
                        COptionLabel.Visible
                            = COptionTB.Visible = AddCBButton.Visible = PollRBList.Visible = ROptionLabel.Visible
                                = ROptionTB.Visible = AddRBButton.Visible = false;
                    FileL.Visible = FileUpload.Visible = true;*/
                }
            }
        }

        protected void PollTypeDDL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                bool option = bool.Parse(PollTypeDDL.SelectedValue);
                CPanel.Visible = !option;
                RPanel.Visible = option;
                //PollCBList.Visible = COptionLabel.Visible = COptionTB.Visible = AddCBButton.Visible = !option;
                //PollRBList.Visible = ROptionLabel.Visible = ROptionTB.Visible = AddRBButton.Visible = option;
            }
        }

        protected void AddCBButton_OnClick(object sender, EventArgs e)
        {
            if (COptionTB.Text != "" && PollCBList.Items.FindByText(COptionTB.Text) == null)
            {
                PollCBList.Items.Add(new ListItem(COptionTB.Text));
                StatusMsg.Text = "";
                COptionTB.Text = "";    // clear textbox
            }
            else
            {
                StatusMsg.Text = "Invalid option!";
            }
        }

        protected void AddRBButton_OnClick(object sender, EventArgs e)
        {
            if (ROptionTB.Text != "" && PollRBList.Items.FindByText(ROptionTB.Text) == null)
            {
                PollRBList.Items.Add(new ListItem(ROptionTB.Text));
                StatusMsg.Text = "";
                ROptionTB.Text = "";
            }
            else
            {
                StatusMsg.Text = "Invalid option!";
            }
        }
    }
}