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

        int startMonth = 5;
        int startYear = 2019;
        int correctAnswer = 0;
        int questionId;

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

        int GetMonthIdForQuestion(int SM, int SY)
        {
            int resultId = 0;

            DateTime date = DateTime.Now;
            DateTime start = DateTime.Parse($"1 {SM} {SY}");

            resultId = (((date.Year - start.Year) * 12) + date.Month - start.Month) + 1;

            return resultId;
        }

        DataRow[] GetQuestionFromDatabase()
        {
            int monthId = GetMonthIdForQuestion(startMonth, startYear);

            string query = "" +
                "Select * from kristia1_route66.questions " +
                $"Where question_ID = {monthId.ToString()}";

            var rows = sql.GetDataFromDatabase(query);

            return rows;
        }

        void UpdateQuestionOnPage()
        {
            var rows = GetQuestionFromDatabase();

            foreach (DataRow row in rows)
            {
                Question_Text.Text = row["question_TEXT"].ToString();

                AnswerList.Items[0].Text = "&nbsp;&nbsp; " + row["question_ANSWER1"].ToString();
                AnswerList.Items[1].Text = "&nbsp;&nbsp; " + row["question_ANSWER2"].ToString();
                AnswerList.Items[2].Text = "&nbsp;&nbsp; " + row["question_ANSWER3"].ToString();

                correctAnswer = int.Parse(row["question_CORRECT-ANSWER"].ToString());
                questionId = int.Parse(row["question_ID"].ToString());
            }
        }


        protected void SubmitBtn_Clicked(object sender, EventArgs e)
        {

        }

        void PostVisitToDatabase()
        {
            string query = "" +
                "INSERT INTO kritia1_route66.visitors (visitor_FIRSTNAME, visitor_LASTNAME, visitor_EMAIL, visitor_ANSWER_ID, visitor_QUESTION_ID)" +
                $"VALUES ";
        }
    }
}