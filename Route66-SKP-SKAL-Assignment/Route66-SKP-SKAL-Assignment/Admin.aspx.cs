using Route66_SKP_SKAL_Assignment.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Route66_SKP_SKAL_Assignment
{
    public partial class Admin : System.Web.UI.Page
    {
        string getAllVisitors = "SELECT * FROM kristia1_route66.questions";
        public int CurrentSM;
        public int CurrentSY;

        SqlHelper sql = new SqlHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetStart();
                SetDropDowns();
                InsertDataToTable(sql.GetTableFromDatabase(getAllVisitors));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void InsertDataToTable(DataSet set)
        {
            DATA_GRID.DataSource = set.Tables["questions"];
            DATA_GRID.DataBind();
        }

        void SetDropDowns()
        {
            //Month
            MONTH_LIST.Items.Add(new ListItem("January", "1"));
            MONTH_LIST.Items.Add(new ListItem("February", "2"));
            MONTH_LIST.Items.Add(new ListItem("March", "3"));
            MONTH_LIST.Items.Add(new ListItem("April", "4"));
            MONTH_LIST.Items.Add(new ListItem("May", "5"));
            MONTH_LIST.Items.Add(new ListItem("June", "6"));
            MONTH_LIST.Items.Add(new ListItem("July", "7"));
            MONTH_LIST.Items.Add(new ListItem("August", "8"));
            MONTH_LIST.Items.Add(new ListItem("September", "9"));
            MONTH_LIST.Items.Add(new ListItem("October", "10"));
            MONTH_LIST.Items.Add(new ListItem("November", "11"));
            MONTH_LIST.Items.Add(new ListItem("December", "12"));

            // Year
            YEAR_LIST.Items.Add(new ListItem("Current Year", $"{DateTime.Now.Year}"));
            YEAR_LIST.Items.Add(new ListItem($"{CurrentSY - 2}", $"{CurrentSY - 2}"));
            YEAR_LIST.Items.Add(new ListItem($"{CurrentSY - 1}", $"{CurrentSY - 1}"));
            YEAR_LIST.Items.Add(new ListItem($"{CurrentSY}", $"{CurrentSY}"));
            YEAR_LIST.Items.Add(new ListItem($"{CurrentSY + 1}", $"{CurrentSY + 1}"));
            YEAR_LIST.Items.Add(new ListItem($"{CurrentSY + 2}", $"{CurrentSY + 2}"));
        }

        protected void Submitbtn(object sender, EventArgs e)
        {
            try
            {
                SetStart();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void SetStart()
        {
            string query = "" +
                "UPDATE kristia1_route66.startinfo " +
                $"SET info_SM = {int.Parse(MONTH_LIST.SelectedValue)}, info_SY = {int.Parse(YEAR_LIST.SelectedValue)} " +
                "WHERE(info_ID = 1)";

            sql.SetDataToDatabase(query);
        }

        void GetStart()
        {
            string query = "" +
                "Select * from kristia1_route66.startinfo " +
                $"Where info_ID = 1";

            DataRow[] rows = sql.GetDataFromDatabase(query);

            CurrentSM = int.Parse(rows[0]["info_SM"].ToString());
            CurrentSY = int.Parse(rows[0]["info_SY"].ToString());
        }


    }
}