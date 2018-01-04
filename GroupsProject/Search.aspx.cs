using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && Request.Params["query"] != null)
        {
            string query = Server.UrlDecode(Request.Params["query"]);

            SqlDataSource1.SelectCommand = "SELECT DISTINCT Groups.GroupId AS 'GID'," +
                                           " Groups.GroupName AS 'Group Name'," +
                                           " Groups.GroupDescription AS 'Description'" +
                                           " FROM[Groups]" +
                                           " INNER JOIN[GroupsLists]" +
                                           " ON Groups.GroupId = GroupsLists.GroupId" +
                                           " WHERE GroupName LIKE @query OR GroupDescription LIKE @query";

            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("query", "%" + query + "%");
            SqlDataSource1.DataBind();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx?query=" + Server.UrlEncode(SearchTextBox.Text));
    }
}