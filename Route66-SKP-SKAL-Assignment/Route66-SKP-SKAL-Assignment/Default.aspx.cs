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
    public partial class _Default : Page
    {
        readonly SqlHelper sql = new SqlHelper();

        int startMonth = 6;
        int startYear = 2019;
        int correctAnswer1 = 0;
        int correctAnswer2 = 0;
        int correctAnswer3 = 0;
        int questionId1;
        int questionId2;
        int questionId3;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetStart();
                UpdateQuestionOnPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void Test()
        {
            string query = "" +
                "Insert into test (testcol)" +
                $"values ('Hello this is a Test');";

            sql.SetDataToDatabase(query);
        }

        void GetStart()
        {
            string query = "" +
                "Select * from kristia1_route66.startinfo " +
                $"Where info_ID = 1";

            DataRow[] rows = sql.GetDataFromDatabase(query);

            startMonth = int.Parse(rows[0]["info_SM"].ToString());
            startYear = int.Parse(rows[0]["info_SY"].ToString());
        }

        int[] GetMonthIdForQuestion(int SM, int SY)
        {
            int[] resultId = new int[3];

            DateTime date = DateTime.Now;
            DateTime start = DateTime.Parse($"1 {SM} {SY}");

            int monthId = (((date.Year - start.Year) * 12) + date.Month - start.Month) + 1;

            int calc = 1;

            if (monthId != 1)
            {
                calc = (2 * monthId);
            }

            resultId[0] = 0 + calc;
            resultId[1] = 1 + calc;
            resultId[2] = 2 + calc;

            return resultId;
        }

        DataRow[] GetQuestionFromDatabase()
        {
            int[] monthIds = GetMonthIdForQuestion(startMonth, startYear);

            List<DataRow> rowList = new List<DataRow>();

            string[] querys = new string[]
            {
                "" +
                "Select * from kristia1_route66.questions " +
                $"Where question_ID = {monthIds[0].ToString()}",

                "" +
                "Select * from kristia1_route66.questions " +
                $"Where question_ID = {monthIds[1].ToString()}",

                "" +
                "Select * from kristia1_route66.questions " +
                $"Where question_ID = {monthIds[2].ToString()}"
            };

            foreach (string query in querys)
            {
                var queryRows = sql.GetDataFromDatabase(query);

                foreach (DataRow row in queryRows)
                {
                    rowList.Add(row);
                }

            }

            DataRow[] rows = rowList.ToArray();

            return rows;
        }

        void UpdateQuestionOnPage()
        {
            var rows = GetQuestionFromDatabase();

            int i = 0;

            foreach (DataRow row in rows)
            {
                i++;

                switch (i)
                {
                    case 1:
                        {
                            Question_Text1.Text = row["question_TEXT"].ToString();

                            AnswerList1.Items[0].Text = "&nbsp;&nbsp; " + row["question_ANSWER1"].ToString();
                            AnswerList1.Items[1].Text = "&nbsp;&nbsp; " + row["question_ANSWER2"].ToString();
                            AnswerList1.Items[2].Text = "&nbsp;&nbsp; " + row["question_ANSWER3"].ToString();

                            correctAnswer1 = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                            questionId1 = int.Parse(row["question_ID"].ToString());
                            break;
                        }
                    case 2:
                        {
                            Question_Text2.Text = row["question_TEXT"].ToString();

                            AnswerList2.Items[0].Text = "&nbsp;&nbsp; " + row["question_ANSWER1"].ToString();
                            AnswerList2.Items[1].Text = "&nbsp;&nbsp; " + row["question_ANSWER2"].ToString();
                            AnswerList2.Items[2].Text = "&nbsp;&nbsp; " + row["question_ANSWER3"].ToString();

                            correctAnswer2 = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                            questionId2 = int.Parse(row["question_ID"].ToString());
                            break;
                        }
                    case 3:
                        {
                            Question_Text3.Text = row["question_TEXT"].ToString();

                            AnswerList3.Items[0].Text = "&nbsp;&nbsp; " + row["question_ANSWER1"].ToString();
                            AnswerList3.Items[1].Text = "&nbsp;&nbsp; " + row["question_ANSWER2"].ToString();
                            AnswerList3.Items[2].Text = "&nbsp;&nbsp; " + row["question_ANSWER3"].ToString();

                            correctAnswer3 = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                            questionId3 = int.Parse(row["question_ID"].ToString());
                            break;
                        }
                }
            }
        }


        protected void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                PostVisitToDatabase();
            }
            catch (Exception ex)
            {
                WriteSubmissionError(ex);
            }
        }

        int GetSubmissionAnswerID(int questionNum)
        {
            int result = 2;

            switch (questionNum)
            {
                case 1:
                    {
                        if ((AnswerList1.SelectedIndex + 1) == correctAnswer1)
                        {
                            result = 1;
                        }
                        break;
                    }
                case 2:
                    {
                        if ((AnswerList2.SelectedIndex + 1) == correctAnswer2)
                        {
                            result = 1;
                        }
                        break;
                    }
                case 3:
                    {
                        if ((AnswerList3.SelectedIndex + 1) == correctAnswer3)
                        {
                            result = 1;
                        }
                        break;
                    }
            }

            return result;
        }

        bool VerifyInputTextboxs()
        {
            bool result = false;

            if (AnswerList1.SelectedIndex != -1)
            {
                if (AnswerList2.SelectedIndex != -1)
                {
                    if (AnswerList3.SelectedIndex != -1)
                    {
                        if (!string.IsNullOrWhiteSpace(FIRSTNAME_TEXTBOX.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(LASTNAME_TEXTBOX.Text))
                            {
                                if (!string.IsNullOrWhiteSpace(EMAIL_TEXTBOX.Text))
                                {
                                    if (EMAIL_TEXTBOX.Text.Contains('@'))
                                    {
                                        if (EMAIL_TEXTBOX.Text.Contains('.'))
                                        {
                                            result = true;
                                        }
                                        else
                                        {
                                            throw new Exception("The '.' is missing in E-Mail");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("The '@' is missing in E-Mail");
                                    }
                                }
                                else
                                {
                                    throw new Exception("E-Mail is empty");
                                }
                            }
                            else
                            {
                                throw new Exception("Lastname is empty");
                            }
                        }
                        else
                        {
                            throw new Exception("Firstname is empty");
                        }
                    }
                    else
                    {
                        throw new Exception("Question 3 is un-answered");
                    }
                }
                else
                {
                    throw new Exception("Question 2 is un-answered");
                }
            }
            else
            {
                throw new Exception("Question 1 is un-answered");
            }

            return result;
        }

        bool VerifyTicketNotExists(string eMail, int[] questionIDs)
        {
            bool verified = false;

            string query = "" +
                "Select * from kristia1_route66.visitors " +
                $"Where visitor_EMAIL = {eMail} AND visitor_QUESTION_ID = {questionId1} OR visitor_QUESTION_ID = {questionId2} OR visitor_QUESTION_ID = {questionId3}";

            verified = sql.CheckDataFromDatabase(query);

            if(!verified)
            {
                return !verified;
            }
            else
            {
                throw new Exception("E-Mail already used for these questions");
            }
        }

        void WriteSubmissionError(Exception error)
        {
            Debug.WriteLine(error);
            ERROR_LABEL.Text = error.Message;
        }

        string CustomIdGen()
        {
            Random random = new Random();

            string result = "";

            string[] ID = DateTime.Now.ToString().Split(' ');

            result = $"{ID[0].Replace("-","")}-{ID[1].Replace(":","")}-{random.Next(1111, 9999)}";

            return result;
        }

        void PostVisitToDatabase()
        {

            List<int> ids = new List<int>();

            ids.Add(questionId1);
            ids.Add(questionId2);
            ids.Add(questionId3);

            if (VerifyInputTextboxs())
            {
                if (VerifyTicketNotExists(EMAIL_TEXTBOX.Text, ids.ToArray()))
                {
                    string ID = CustomIdGen();

                    // INSERT INTO `kristia1_route66`.`visitors` (`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, `visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) VALUES ('asdad', 'asdasdas', 'saasdasd', 'asdafsdg', '1', '5');
                    string[] querys = new string[]
                    {
                    //1
                    "INSERT INTO `kristia1_route66`.`visitors` " +
                    "(`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, " +
                    "`visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) " +
                    "VALUES " +
                    $"('{FIRSTNAME_TEXTBOX.Text}', '{LASTNAME_TEXTBOX.Text}', '{EMAIL_TEXTBOX.Text}', " +
                    $"'{ID}', {GetSubmissionAnswerID(questionId1)}, {questionId1});",

                    //2
                    "INSERT INTO `kristia1_route66`.`visitors` " +
                    "(`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, " +
                    "`visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) " +
                    "VALUES " +
                    $"('{FIRSTNAME_TEXTBOX.Text}', '{LASTNAME_TEXTBOX.Text}', '{EMAIL_TEXTBOX.Text}', " +
                    $"'{ID}', {GetSubmissionAnswerID(questionId2)}, {questionId2});",

                    //3
                    "INSERT INTO `kristia1_route66`.`visitors` " +
                    "(`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, " +
                    "`visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) " +
                    "VALUES " +
                    $"('{FIRSTNAME_TEXTBOX.Text}', '{LASTNAME_TEXTBOX.Text}', '{EMAIL_TEXTBOX.Text}', " +
                    $"'{ID}', {GetSubmissionAnswerID(questionId3)}, {questionId3});",
                    };

                    foreach (string query in querys)
                    {
                        sql.SetDataToDatabase(query);
                    }
                }
            }
        }
    }
}