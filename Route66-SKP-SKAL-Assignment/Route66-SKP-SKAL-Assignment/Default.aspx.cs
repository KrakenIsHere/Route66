using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using Route66_SKP_SKAL_Assignment.Scripts.Helpers;

namespace Route66_SKP_SKAL_Assignment
{
    public partial class Default : Page
    {
        private readonly SqlHelper _sql = new SqlHelper();

        private int _startMonth = 6;
        private int _startYear = 2019;
        private int _correctAnswer1;
        private int _correctAnswer2;
        private int _correctAnswer3;
        private int _questionId1;
        private int _questionId2;
        private int _questionId3;

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

        private void GetStart()
        {
            var query = "" +
                        "Select * from route66.startinfo " +
                        "Where info_ID = 1";

            var rows = _sql.GetDataFromDatabase(query);

            _startMonth = int.Parse(rows[0]["info_SM"].ToString());
            _startYear = int.Parse(rows[0]["info_SY"].ToString());
        }

        private IEnumerable<DataRow> GetQuestionFromDatabase()
        {
            var monthIds = CalculationHelper.GetMonthIdForQuestion(_startMonth, _startYear);

            var queries = new[]
            {
                "" +
                "Select * from route66.questions " +
                $"Where question_ID = {monthIds[0].ToString()}",

                "" +
                "Select * from route66.questions " +
                $"Where question_ID = {monthIds[1].ToString()}",

                "" +
                "Select * from route66.questions " +
                $"Where question_ID = {monthIds[2].ToString()}"
            };

            var rows = queries.SelectMany(query => _sql.GetDataFromDatabase(query)).ToArray();

            return rows;
        }

        private void UpdateQuestionOnPage()
        {
            var rows = GetQuestionFromDatabase();

            var i = 0;

            foreach (var row in rows)
            {
                i++;

                switch (i)
                {
                    case 1:
                    {
                        Question_Text1.Text = row["question_TEXT"].ToString();

                        AnswerList1.Items[0].Text = @"&nbsp;&nbsp; " + row["question_ANSWER1"];
                        AnswerList1.Items[1].Text = @"&nbsp;&nbsp; " + row["question_ANSWER2"];
                        AnswerList1.Items[2].Text = @"&nbsp;&nbsp; " + row["question_ANSWER3"];

                        _correctAnswer1 = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                        _questionId1 = int.Parse(row["question_ID"].ToString());
                        break;
                    }

                    case 2:
                    {
                        Question_Text2.Text = row["question_TEXT"].ToString();

                        AnswerList2.Items[0].Text = @"&nbsp;&nbsp; " + row["question_ANSWER1"];
                        AnswerList2.Items[1].Text = @"&nbsp;&nbsp; " + row["question_ANSWER2"];
                        AnswerList2.Items[2].Text = @"&nbsp;&nbsp; " + row["question_ANSWER3"];

                        _correctAnswer2 = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                        _questionId2 = int.Parse(row["question_ID"].ToString());
                        break;
                    }

                    case 3:
                    {
                        Question_Text3.Text = row["question_TEXT"].ToString();

                        AnswerList3.Items[0].Text = @"&nbsp;&nbsp; " + row["question_ANSWER1"];
                        AnswerList3.Items[1].Text = @"&nbsp;&nbsp; " + row["question_ANSWER2"];
                        AnswerList3.Items[2].Text = @"&nbsp;&nbsp; " + row["question_ANSWER3"];

                        _correctAnswer3 = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                        _questionId3 = int.Parse(row["question_ID"].ToString());
                        break;
                    }

                    default:
                        return;
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

        private int GetSubmissionAnswerId(int questionNum)
        {
            var result = 2;

            switch (questionNum)
            {
                case 1:
                {
                    if ((AnswerList1.SelectedIndex + 1) == _correctAnswer1)
                    {
                        result = 1;
                    }

                    break;
                }

                case 2:
                {
                    if ((AnswerList2.SelectedIndex + 1) == _correctAnswer2)
                    {
                        result = 1;
                    }

                    break;
                }

                case 3:
                {
                    if ((AnswerList3.SelectedIndex + 1) == _correctAnswer3)
                    {
                        result = 1;
                    }

                    break;
                }

                default:
                    return 0;
            }

            return result;
        }

        private bool VerifyInputTextboxes()
        {
            if (AnswerList1.SelectedIndex == -1) throw new Exception("Question 1 is un-answered");

            if (AnswerList2.SelectedIndex == -1) throw new Exception("Question 2 is un-answered");

            if (AnswerList3.SelectedIndex == -1) throw new Exception("Question 3 is un-answered");

            if (string.IsNullOrWhiteSpace(FIRSTNAME_TEXTBOX.Text))
                throw new Exception("Firstname is empty");

            if (string.IsNullOrWhiteSpace(LASTNAME_TEXTBOX.Text))
                throw new Exception("Lastname is empty");

            if (string.IsNullOrWhiteSpace(EMAIL_TEXTBOX.Text))
                throw new Exception("E-Mail is empty");

            if (!EMAIL_TEXTBOX.Text.Contains('@'))
                throw new Exception("The '@' is missing in E-Mail");

            if (EMAIL_TEXTBOX.Text.Contains('.'))
            {
                return true;
            }

            throw new Exception("The '.' is missing in E-Mail");
        }

        private bool VerifyTicketNotExists(string eMail, IReadOnlyList<int> questionIDs)
        {
            var query = "" +
                        "Select * from route66.visitors " +
                        $"Where visitor_EMAIL = {eMail} AND visitor_QUESTION_ID = {questionIDs[0]} OR visitor_QUESTION_ID = {questionIDs[1]} OR visitor_QUESTION_ID = {questionIDs[2]}";

            var verified = _sql.CheckDataFromDatabase(query);

            if (!verified)
            {
                return true;
            }

            throw new Exception("E-Mail already used for these questions");
        }

        private void WriteSubmissionError(Exception error)
        {
            Debug.WriteLine(error);
            ERROR_LABEL.Text = error.Message;
        }

        private static string CustomIdGen()
        {
            var random = new Random();

            var id = DateTime.Now.ToString(CultureInfo.CurrentCulture).Split(' ');

            var result = $"{id[0].Replace("-", "")}-{id[1].Replace(":", "")}-{random.Next(1111, 9999)}";

            return result;
        }

        private void PostVisitToDatabase()
        {
            var ids = new List<int> {_questionId1, _questionId2, _questionId3};


            if (!VerifyInputTextboxes()) return;
            if (!VerifyTicketNotExists(EMAIL_TEXTBOX.Text, ids.ToArray())) return;
            var id = CustomIdGen();

            // INSERT INTO `route66`.`visitors` (`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, `visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) VALUES ('kim', 'johnson', 'kimjohnson804@gmail.com', 'Ao4tUX', '1', '5');
            var queries = new[]
            {
                //1
                "INSERT INTO `route66`.`visitors` " +
                "(`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, " +
                "`visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) " +
                "VALUES " +
                $"('{FIRSTNAME_TEXTBOX.Text}', '{LASTNAME_TEXTBOX.Text}', '{EMAIL_TEXTBOX.Text}', " +
                $"'{id}', {GetSubmissionAnswerId(_questionId1)}, {_questionId1});",

                //2
                "INSERT INTO `route66`.`visitors` " +
                "(`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, " +
                "`visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) " +
                "VALUES " +
                $"('{FIRSTNAME_TEXTBOX.Text}', '{LASTNAME_TEXTBOX.Text}', '{EMAIL_TEXTBOX.Text}', " +
                $"'{id}', {GetSubmissionAnswerId(_questionId2)}, {_questionId2});",

                //3
                "INSERT INTO `route66`.`visitors` " +
                "(`visitor_FIRSTNAME`, `visitor_LASTNAME`, `visitor_EMAIL`, " +
                "`visitor_CUSTOM-ID`, `visitor_ANSWER_ID`, `visitor_QUESTION_ID`) " +
                "VALUES " +
                $"('{FIRSTNAME_TEXTBOX.Text}', '{LASTNAME_TEXTBOX.Text}', '{EMAIL_TEXTBOX.Text}', " +
                $"'{id}', {GetSubmissionAnswerId(_questionId3)}, {_questionId3});",
            };

            foreach (var query in queries)
            {
                _sql.SetDataToDatabase(query);
            }
        }
    }
}