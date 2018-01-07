using System;

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
            " CategoryName LIKE @query ";
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
            SqlDataSource1.SelectParameters.Add("query", "%" + query + "%");
            SqlDataSource1.DataBind();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx?query=" + Server.UrlEncode(SearchTextBox.Text));
    }
}