using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
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
                if (PostTypeDDL.SelectedValue == "message")
                {
                    MessagePanel.Visible = true;
                    PollPanel.Visible = FilePanel.Visible = false;
                }
                else if (PostTypeDDL.SelectedValue == "poll")
                {
                    MessagePanel.Visible = FilePanel.Visible = false;
                    PollPanel.Visible = true;
                }
                else
                {
                    MessagePanel.Visible = PollPanel.Visible = false;
                    FilePanel.Visible = true;
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

        protected void PostButton_OnClick(object sender, EventArgs e)
        {
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

                    string query = "INSERT INTO Posts" +
                                   " (PostType, GroupId, PostDate, UserName)" +
                                   " VALUES (@type, @gid, @date, @uname);" +
                                   " SELECT CAST(scope_identity() AS int)";
                    var type = PostTypeDDL.SelectedValue;                    
                    DateTime date = DateTime.UtcNow;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("type", type);
                    cmd.Parameters.AddWithValue("gid", gid);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("uname", User.Identity.Name);
                    int postId = (int) cmd.ExecuteScalar();
                    switch (type)
                    {
                        case "message":
                            string mq = "INSERT INTO Messages" +
                                        " (MessageTitle, MessageContent, PostId)" +
                                        " VALUES (@mtitle, @mcontent, @postId)";
                            SqlCommand mc = new SqlCommand(mq, con);
                            mc.Parameters.AddWithValue("mtitle", MsgTitle.Text);
                            mc.Parameters.AddWithValue("mcontent", MsgContentTB.Text);
                            mc.Parameters.AddWithValue("postId", postId);
                            mc.ExecuteNonQuery();
                            break;
                        case "poll":    // todo
                            string pq = "INSERT INTO Polls" +
                                        " (PollType, PollQuestion, PostId)" +
                                        " VALUES (@pt, @pq, @postId);" +
                                        " SELECT CAST(scope_identity() AS int)";





                            string mlist = "SELECT UserName FROM GroupsLists WHERE GroupId = @gid";
                            SqlCommand mlc = new SqlCommand(mlist, con);
                            mlc.Parameters.AddWithValue("gid", gid);
                            reader = mlc.ExecuteReader();
                            var members = new List<string>();
                            while (reader.Read())
                            {
                                members.Add(reader["UserName"].ToString());
                            }
                            reader.Close();
                            
                            SqlCommand pc = new SqlCommand(pq, con);
                            
                            var pollType = bool.Parse(PollTypeDDL.SelectedValue);
                            pc.Parameters.AddWithValue("pt", pollType);
                            pc.Parameters.AddWithValue("pq", PollQTB.Text);
                            pc.Parameters.AddWithValue("postId", postId);
                            int pollId = (int) pc.ExecuteScalar();


                            foreach (string member in members)
                            {
                                string paq = "INSERT INTO PollsAnswers" +
                                             " (Answered, UserName, PollId)" +
                                             " VALUES (@ans, @uname, @pollId)";
                                SqlCommand pac = new SqlCommand(paq, con);
                                pac.Parameters.AddWithValue("ans", false);
                                pac.Parameters.AddWithValue("uname", member);
                                pac.Parameters.AddWithValue("pollId", pollId);
                                pac.ExecuteNonQuery();
                            }
                            
                            CheckBoxList list;
                            if (pollType) // single choice
                            {
                                list = PollRBList;
                            }
                            else
                            {
                                list = PollCBList;
                            }
                            foreach (var item in list.Items)
                            {
                                string oq = "INSERT INTO Options" +
                                            " (OptionName, PollId, OptionCount)" +
                                            " VALUES (@oname, @pollId, @ocount)";
                                SqlCommand oc = new SqlCommand(oq, con);
                                oc.Parameters.AddWithValue("oname", item.ToString());
                                oc.Parameters.AddWithValue("pollId", pollId);
                                oc.Parameters.AddWithValue("ocount", 0);
                                oc.ExecuteNonQuery();
                            }

                            break;
                        case "file":
                            if(!FileUpload.HasFile)
                            {
                                StatusMsg.Text = "No file selected!";
                                throw new Exception();
                            }
                            
                            Stream fs = FileUpload.PostedFile.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            Byte[] bytes = br.ReadBytes((int)fs.Length);

                            string fq = "INSERT INTO Files" +
                                        " (FileName, FileContent, PostId, FileType)" +
                                        " VALUES (@fname, @fcontent, @postId, @fType)";
                            string fileName = Path.GetFileName(FileUpload.PostedFile.FileName);
                            string ext = Path.GetExtension(fileName);
                            string fType;
                            switch (ext)
                            {
                                case ".doc":
                                    fType = "application/vnd.ms-word";
                                    break;
                                case ".docx":
                                    fType = "application/vnd.ms-word";
                                    break;
                                case ".xls":
                                    fType = "application/vnd.ms-excel";
                                    break;
                                case ".xlsx":
                                    fType = "application/vnd.ms-excel";
                                    break;
                                case ".jpg":
                                    fType = "image/jpg";
                                    break;
                                case ".png":
                                    fType = "image/png";
                                    break;
                                case ".gif":
                                    fType = "image/gif";
                                    break;
                                case ".pdf":
                                    fType = "application/pdf";
                                    break;
                                case ".txt":
                                    fType = "text/plain";
                                    break;
                                default:
                                    fType = ext;
                                    break;
                            }
                            
                            SqlCommand fc = new SqlCommand(fq, con);
                            fc.Parameters.AddWithValue("fname", fileName);
                            fc.Parameters.AddWithValue("fcontent", bytes);
                            fc.Parameters.AddWithValue("postId", postId);
                            fc.Parameters.AddWithValue("fType", fType);
                            fc.ExecuteNonQuery();
                            break;
                        //default:
                            //break;
                        
                    }
                    Response.Redirect("~/GroupPage.aspx?gid=" + Server.UrlEncode(gid));
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
        
        protected void DelCBButton_OnClick(object sender, EventArgs e)
        {
            List<ListItem> list = new List<ListItem>();
            for (int i = 0; i < PollCBList.Items.Count; i++)
            {
                if (PollCBList.Items[i].Selected)
                {
                    list.Add(PollCBList.Items[i]);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                PollCBList.Items.Remove(list[i]);
            }
        }
        
        protected void DelRBButton_OnClick(object sender, EventArgs e)
        {
            List<ListItem> list = new List<ListItem>();
            for (int i = 0; i < PollRBList.Items.Count; i++)
            {
                if (PollRBList.Items[i].Selected)
                {
                    list.Add(PollRBList.Items[i]);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                PollRBList.Items.Remove(list[i]);
            }
        }
    }
}