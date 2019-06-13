using MySql.Web.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Route66_SKP_SKAL_Assignment
{
    public partial class Login : System.Web.UI.Page
    {
        readonly MySQLMembershipProvider provider = new MySQLMembershipProvider();
        protected void Login_Click(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                try
                {
                    var isValid = provider.ValidateUser(USERNAME_TEXT.Text, PASSWORD_TEXT.Text);

                    ERROR_LABEL.Text = isValid.ToString();

                    if (isValid)
                    {
                        Session["username"] = USERNAME_TEXT.Text;
                        Response.Redirect("/Admin");
                    }
                    else
                    {
                        ERROR_LABEL.Text = "Wrong Username or Password";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else
            {
                Response.Redirect("/Admin");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}