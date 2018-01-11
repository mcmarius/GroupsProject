using System;
using System.Web.UI.WebControls;

namespace UserPages
{
    public partial class MyGroups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void SqlDataSource1_OnSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@UserName"].Value = User.Identity.Name;
        }
    }
}