using System;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            var l = LoginView1.FindControl("NewCategoryLabel");
            var nc = LoginView1.FindControl("NewCategory");
            var cl = LoginView1.FindControl("CategoryList");
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
}