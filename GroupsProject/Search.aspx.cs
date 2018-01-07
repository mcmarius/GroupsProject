using System;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && Request.Params["query"] != null)
        {
            string query = Server.UrlDecode(Request.Params["query"]);
            SqlDataSource1.SelectCommand = "SELECT DISTINCT Groups.GroupId, Groups.GroupName, Groups.GroupDescription, Groups.CategoryId, CategoryName"+
            " FROM[Groups]" +
            " INNER JOIN[GroupsLists] ON Groups.GroupId = GroupsLists.GroupId" +
            " INNER JOIN[Categories] ON Groups.CategoryId = Categories.CategoryId" +
            " WHERE"+
            " GroupName LIKE @query OR GroupDescription LIKE @query OR" +
            " CategoryName = N'@query' OR CategoryName LIKE @query ";
            //SqlDataSource1.SelectCommand = "SELECT DISTINCT Groups.GroupId," +
            //                               " Groups.GroupName," +
            //                               " Groups.GroupDescription " +
            //                               " Groups.CategoryId " +
            //                               " FROM [Groups] " +
            //                               " INNER JOIN [GroupsLists] ON Groups.GroupId = GroupsLists.GroupId" +
            //                               " INNER JOIN [Categories] ON Groups.CategoryId = Categories.CategoryId" +
            //                               " WHERE GroupName LIKE @query OR" +
            //                               " GroupDescription LIKE @query OR" +
            //                               " CategoryName LIKE @query";

            SqlDataSource1.SelectParameters.Clear();
            if (bool.Parse(Server.UrlDecode(Request.Params["s"])))
            {
                SqlDataSource1.SelectParameters.Add("query", "" + query + "");
            }
            else
            {
                SqlDataSource1.SelectParameters.Add("query", "%" + query + "%");
            }
            SqlDataSource1.DataBind();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx?s=" + Server.UrlEncode(StrictCB.Checked.ToString())
                          + "&query=" + Server.UrlEncode(SearchTextBox.Text));
    }
}