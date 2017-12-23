using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateUserWizard1_OnCreatedAdmin(object sender, EventArgs e)
    {
        Roles.AddUserToRole(CreateAdminWizard.UserName, "Admin");
    }
}