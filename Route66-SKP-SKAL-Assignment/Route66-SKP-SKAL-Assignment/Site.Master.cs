using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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