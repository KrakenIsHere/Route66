using System;
using System.Web.UI;
using System.Diagnostics;
using MySql.Web.Security;

namespace Route66_SKP_SKAL_Assignment
{
    public partial class Login : Page
    {
        private readonly MySQLMembershipProvider _provider = new MySQLMembershipProvider();
        protected void Login_Click(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                try
                {
                    var isValid = _provider.ValidateUser(USERNAME_TEXT.Text, PASSWORD_TEXT.Text);

                    ERROR_LABEL.Text = isValid.ToString();

                    if (isValid)
                    {
                        Session["username"] = USERNAME_TEXT.Text;
                        Response.RedirectToRoute("AdminPanel");
                    }
                    else
                    {
                        ERROR_LABEL.Text = @"Wrong Username or Password";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else
            {
                Response.RedirectToRoute("AdminPanel");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}