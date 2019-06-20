using System;
using System.Web.UI;

namespace Route66_SKP_SKAL_Assignment
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                LOGIN_PANEL.CssClass = "";
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Remove("username");
            Response.Redirect("");
        }
    }
}