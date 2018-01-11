using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace AdminPages
{
    public partial class CreateAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_OnCreatedAdmin(object sender, EventArgs e)
        {
            var createAdminWizard = LV.FindControl("_CreateAdminWizard") as CreateUserWizard;
            if (createAdminWizard != null) Roles.AddUserToRole(createAdminWizard.UserName, "Admin");
        }
    }
}