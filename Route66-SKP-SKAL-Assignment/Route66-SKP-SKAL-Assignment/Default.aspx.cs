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
                UpdateQuestionOnPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
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
                            Question_Text1.Text = row["question_TEXT"].ToString();

                            AnswerList2.Items[0].Text = "&nbsp;&nbsp; " + row["question_ANSWER1"].ToString();
                            AnswerList2.Items[1].Text = "&nbsp;&nbsp; " + row["question_ANSWER2"].ToString();
                            AnswerList2.Items[2].Text = "&nbsp;&nbsp; " + row["question_ANSWER3"].ToString();

                            correctAnswer2 = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                            questionId2 = int.Parse(row["question_ID"].ToString());
                            break;
                        }
                    case 3:
                        {
                            Question_Text1.Text = row["question_TEXT"].ToString();

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
                        if (AnswerList1.SelectedIndex + 1 == correctAnswer1)
                        {
                            result = 1;
                        }
                        break;
                    }
                case 2:
                    {
                        if (AnswerList2.SelectedIndex + 1 == correctAnswer2)
                        {
                            result = 1;
                        }
                        break;
                    }
                case 3:
                    {
                        if (AnswerList3.SelectedIndex + 1 == correctAnswer3)
                        {
                            result = 1;
                        }
                        break;
                    }
            }

            return result;
        }

        bool VarifyInputTextboxs()
        {
            bool result = false;

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

            return result;
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

            result = $"{ID[0]}-{ID[1]}-{random.Next(1111, 9999)}";

            return result;
        }

        void PostVisitToDatabase()
        {

            if (VarifyInputTextboxs())
            {
                string ID = CustomIdGen();

                string query = "" +
                    "INSERT INTO kritia1_route66.visitors (visitor_FIRSTNAME, visitor_LASTNAME, visitor_EMAIL, visitor_CUSTOM-ID, visitor_ANSWER_ID, visitor_QUESTION_ID)" +
                    $"VALUES {FIRSTNAME_TEXTBOX.Text}, {LASTNAME_TEXTBOX.Text}, {EMAIL_TEXTBOX.Text}, {ID}, {GetSubmissionAnswerID(1)}, {questionId1}" +
                    "" +
                    "INSERT INTO kritia1_route66.visitors (visitor_FIRSTNAME, visitor_LASTNAME, visitor_EMAIL, visitor_CUSTOM-ID, visitor_ANSWER_ID, visitor_QUESTION_ID)" +
                    $"VALUES {FIRSTNAME_TEXTBOX.Text}, {LASTNAME_TEXTBOX.Text}, {EMAIL_TEXTBOX.Text}, {ID}, {GetSubmissionAnswerID(2)}, {questionId2}" +
                    "" +
                    "INSERT INTO kritia1_route66.visitors (visitor_FIRSTNAME, visitor_LASTNAME, visitor_EMAIL, visitor_CUSTOM-ID, visitor_ANSWER_ID, visitor_QUESTION_ID)" +
                    $"VALUES {FIRSTNAME_TEXTBOX.Text}, {LASTNAME_TEXTBOX.Text}, {EMAIL_TEXTBOX.Text}, {ID}, {GetSubmissionAnswerID(3)}, {questionId3}";

                sql.SetDataToDatabase(query);
            }
        }
    }
}