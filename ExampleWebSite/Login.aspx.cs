using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {

	}

	protected void login_Authenticate(object sender, AuthenticateEventArgs e) {
		e.Authenticated = FormsAuthentication.Authenticate(login.UserName, login.Password);
		if (e.Authenticated) {
			FormsAuthentication.RedirectFromLoginPage(login.UserName, login.RememberMeSet);
		}
	}
}