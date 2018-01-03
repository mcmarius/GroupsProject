using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class MyGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(User.Identity.IsAuthenticated) {
            //var dataSource = LVMG.FindControl("SqlDataSource1") as SqlDataSource;
            //dataSource.SelectParameters.Add("@UserName", DbType.String, User.Identity.Name);
            //dataSource.SelectCommand = "SELECT [GroupName] FROM [Groups] INNER JOIN [GroupsLists] ON Groups.GroupId = GroupsLists.GroupId WHERE UserName = @UserName";
        //}
    }

    protected void SqlDataSource1_OnSelecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@UserName"].Value = User.Identity.Name;
    }
}