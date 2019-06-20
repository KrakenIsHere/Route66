using System;
using System.Data;
using System.Web.UI;
using System.Net.Mail;
using System.Threading;
using System.Diagnostics;
using System.Web.Security;
using System.Web.UI.WebControls;
using Route66_SKP_SKAL_Assignment.Scripts.Helpers;

namespace Route66_SKP_SKAL_Assignment
{
    public partial class Admin : Page
    {
        private const string GetAllVisitors = "SELECT * FROM kristia1_route66.`all-visitors`;"; //Used as is

        readonly string getShowOnlyCorrect = "" + //Used as is
            "SELECT * FROM kristia1_route66.`all-visitors`" +
            "WHERE ANSWER_ID = 1";

        readonly string getShowOnlyCorrectFromQuestion = "" + //Needs extra input
            "SELECT * FROM kristia1_route66.`all-visitors`" +
            "WHERE ANSWER_ID = 1 AND QUESTION_ID = ";

        readonly string getShowOnlyFromQuestion = "" + //Needs extra input
            "SELECT * FROM kristia1_route66.`all-visitors`" +
            "WHERE QUESTION_ID = ";

        readonly string getShowOnlyFromMonth = "" + //Needs extra input
            "SELECT * FROM `kristia1_route66`.`all-visitors`" +
            "WHERE `CUSTOM-ID` LIKE '%:%' ";


        //private int _currentMq1Id;
        //private int _currentMq2Id;
        //private int _currentMq3Id;

        private int _currentSm;
        private int _currentSy;

        private readonly SqlHelper _sql = new SqlHelper();

        protected void SubmitUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Membership.CreateUser(USERNAME_TEXT.Text, PASSWORD_TEXT.Text, EMAIL_TEXT.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void SendMessage_Click(object sender, EventArgs e)
        {
            SendEmail(txtBody.Text, txtFrom.Text, txtTo.Text, txtSubject.Text);
        }

        private void SendEmail(string body, string from, string to, string sub = "")
        {
            Label1.Text = @"Sending Mail Please Wait...";

            Thread.Sleep(500);
            try
            {
                var smtpClient = new SmtpClient();

                var mailMessage = new MailMessage(from, to)
                {
                    Subject = sub,
                    Body = body
                };

                smtpClient.Send(mailMessage);
                Label1.Text = @"Message sent";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Label1.Text = ex.ToString();
            }
        }

        protected void SearchAllAnswersByMonthBtn(object sender, EventArgs e)
        {
            try
            {
                InsertDataToTable(_sql.GetSetFromDatabase(getShowOnlyFromMonth.Replace(":", QUESTION_MONTHS_DROP.SelectedValue)).Tables[0]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void SearchAllAnswersByQuestionBtn(object sender, EventArgs e)
        {
            try
            {
                InsertDataToTable(_sql.GetSetFromDatabase(getShowOnlyFromQuestion + CORRECT_BY_QUESTION_DROP.SelectedValue).Tables[0]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void SearchAllCorrectAnswersByQuestionBtn(object sender, EventArgs e)
        {
            try
            {
                InsertDataToTable(_sql.GetSetFromDatabase(getShowOnlyCorrectFromQuestion + CORRECT_BY_QUESTION_DROP.SelectedValue).Tables[0]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void SearchAllCorrectAnswersBtn(object sender, EventArgs e)
        {
            try
            {
                InsertDataToTable(_sql.GetSetFromDatabase(getShowOnlyCorrect).Tables[0]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void SearchAllAnswersBtn(object sender, EventArgs e)
        {
            try
            {
                InsertDataToTable(_sql.GetSetFromDatabase(GetAllVisitors).Tables[0]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                try
                {
                    GetStart();
                    InsertDataToTable(_sql.GetSetFromDatabase(GetAllVisitors).Tables[0]);
                    CalculationHelper.GetMonthIdForQuestion(_currentSm, _currentSy);

                    if (!Page.IsPostBack)
                    {
                        SetDropDowns();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else
            {
                Response.RedirectToRoute("LoginPanel");
            }
        }

        private void InsertDataToTable(DataTable table)
        {
            DATA_GRID.DataSource = table;
            DATA_GRID.DataBind();
        }

        private void SetDropDowns()
        {
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("January", "Jan"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("February", "Feb"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("March", "Mar"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("April", "Apr"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("May", "May"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("June", "Jun"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("July", "Jul"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("August", "Aug"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("September", "Sep"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("October", "Oct"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("November", "Nov"));
            QUESTION_MONTHS_DROP.Items.Add(new ListItem("December", "Dec"));

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
            YEAR_LIST.Items.Add(new ListItem($"{_currentSy - 2}", $"{_currentSy - 2}"));
            YEAR_LIST.Items.Add(new ListItem($"{_currentSy - 1}", $"{_currentSy - 1}"));
            YEAR_LIST.Items.Add(new ListItem($"{_currentSy}", $"{_currentSy}"));
            YEAR_LIST.Items.Add(new ListItem($"{_currentSy + 1}", $"{_currentSy + 1}"));
            YEAR_LIST.Items.Add(new ListItem($"{_currentSy + 2}", $"{_currentSy + 2}"));

            const string query = "" +
                                 "SELECT * FROM kristia1_route66.`questions`";

            var queryRows = _sql.GetDataFromDatabase(query);

            foreach (var row in queryRows)
            {
                CORRECT_BY_QUESTION_DROP.Items.Add(new ListItem(row["question_ID"].ToString(), row["question_ID"].ToString()));
            }
        }

        protected void SubmitNewStartBtn(object sender, EventArgs e)
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

        protected void SubmitNewQuestionBtn(object sender, EventArgs e)
        {
            try
            {
                PostNewQuestion();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private int GetQuestionTableSize()
        {
            const string query = "" +
                                 "SELECT * FROM kristia1_route66.`questions`;";

            var rows = _sql.GetDataFromDatabase(query);

            var result = rows.Length;

            return result;
        }

        private static bool VerifyTextboxContent()
        {
            const bool result = false;


            return result;
        }

        private void PostNewQuestion()
        {
            if (!VerifyTextboxContent()) return;
            var nextId = GetQuestionTableSize() + 1;

            var query = "" +
                           "INSERT INTO `kristia1_route66`.`questions`" +
                           "(`question_ID`, `question_TEXT`, " +
                           "`question_ANSWER1`, `question_ANSWER2`, `question_ANSWER3`, " +
                           "`question_CORRECT-ANSWER`) " +
                           "VALUES" +
                           $"{nextId}, '{QUESTION_TEXT.Text}', " +
                           $"'{ANSWER1_TEXT.Text}', '{ANSWER2_TEXT.Text}', '{ANSWER3_TEXT.Text}', " +
                           $"'{ANSWER_DROP.SelectedValue}'";

            _sql.SetDataToDatabase(query);
        }

        private void SetStart()
        {
            var query = "" +
                "UPDATE kristia1_route66.startinfo " +
                $"SET info_SM = {int.Parse(MONTH_LIST.SelectedValue)}, info_SY = {int.Parse(YEAR_LIST.SelectedValue)} " +
                "WHERE(info_ID = 1)";

            _sql.SetDataToDatabase(query);
        }

        private void GetStart()
        {
            var query = "" +
                "Select * from kristia1_route66.startinfo " +
                "Where info_ID = 1";

            var rows = _sql.GetDataFromDatabase(query);

            _currentSm = int.Parse(rows[0]["info_SM"].ToString());
            _currentSy = int.Parse(rows[0]["info_SY"].ToString());
        }


    }
}