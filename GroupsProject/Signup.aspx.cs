using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateUserWizard1_OnCreatedUser(object sender, EventArgs e)
    {
        Roles.AddUserToRole(CreateUserWizard1.UserName, "User");
    }

    protected void CreateUserWizard1_OnCreateUserError(object sender, CreateUserErrorEventArgs e)
    {
        CompleteWizardStep1.Title = "Fail!";
    }

    protected void CreateUserWizard1_OnFinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        
    }
}