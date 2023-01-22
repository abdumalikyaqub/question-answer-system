using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Configuration;

namespace Test_qasysapp.Models.Repository
{
    public class QuestionRepos 
    {
        private List<Question> _questions = new List<Question>();

		public Data db = new Data();
		public string constr { get; set; }
		private string sqlExpression = "crud_question";

        public QuestionRepos()
        {
            constr = db.ConnStr!;
        }
        public List<Question> GetQuestions()
        {
			SqlConnection sqlConnection = new SqlConnection(constr);
			SqlCommand com = new SqlCommand(sqlExpression, sqlConnection);
            com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@query", 2);

			SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
			sqlConnection.Open();
            da.Fill(dt);
            sqlConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {

                _questions.Add(

                    new Question
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        Body = Convert.ToString(dr["Body"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        AuthorId = Convert.ToInt32(dr["AuthorId"]),
                        CreatedAt = Convert.ToDateTime(dr["CreatedAt"])
                    }
                    );
            }
            return _questions;
        }

        public bool AddQuestion(Question question)
        {

            question.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("d"));

			SqlConnection sqlConnection = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", question.Title);
            cmd.Parameters.AddWithValue("@Body", question.Body);
            cmd.Parameters.AddWithValue("@AuthorId", question.AuthorId);
            cmd.Parameters.AddWithValue("@CategoryId", question.CategoryId);
            cmd.Parameters.AddWithValue("@CretedAt", question.CreatedAt);
            cmd.Parameters.AddWithValue("@query", 1);

            sqlConnection.Open();
            int i = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            if (i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool Update(Question question)
		{

			question.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("d"));

			SqlConnection sqlConnection = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@Id", question.Id);
			cmd.Parameters.AddWithValue("@Title", question.Title);
			cmd.Parameters.AddWithValue("@Body", question.Body);
			cmd.Parameters.AddWithValue("@AuthorId", question.AuthorId);
			cmd.Parameters.AddWithValue("@CategoryId", question.CategoryId);
			cmd.Parameters.AddWithValue("@CretedAt", question.CreatedAt);
			cmd.Parameters.AddWithValue("@query", 3);

			sqlConnection.Open();
			int i = cmd.ExecuteNonQuery();
			sqlConnection.Close();
			if (i >= 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool Delete(int id)
		{

			SqlConnection sqlConnection = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@Id", id);
			cmd.Parameters.AddWithValue("@query", 4);

			sqlConnection.Open();
			int i = cmd.ExecuteNonQuery();
			sqlConnection.Close();
			if (i >= 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


	}
}
