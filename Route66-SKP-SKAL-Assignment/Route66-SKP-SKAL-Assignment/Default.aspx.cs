using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Route66_SKP_SKAL_Assignment
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Clicked(object sender, EventArgs e)
        {

        }

        protected void Checked_Changed(object sender, EventArgs e)
        {
            CheckBox thisBox = sender as CheckBox;

            Debug.WriteLine(thisBox.ID);

            switch(thisBox.ID)
            {
                case "ANSWER1":
                    {
                        ANSWER2.Checked = false;
                        ANSWER3.Checked = false;
                        break;
                    }
                case "ANSWER2":
                    {
                        ANSWER1.Checked = false;
                        ANSWER3.Checked = false;
                        break;
                    }
                case "ANSWER3":
                    {
                        ANSWER2.Checked = false;
                        ANSWER1.Checked = false;
                        break;
                    }
            }
        }
    }
}