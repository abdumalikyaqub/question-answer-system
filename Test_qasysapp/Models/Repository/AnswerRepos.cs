using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace Test_qasysapp.Models.Repository
{
    public class AnswerRepos
    {
        public List<Answer> AnswerList { get; set; }
        public Data db = new Data();
        public string constr { get; set; }
		private string sqlExpression = "crud_answer";

		public AnswerRepos()
        {
            constr = db.ConnStr!;
            AnswerList = new List<Answer>();
        }
        public List<Answer> GetAnswers()
        {
            SqlConnection sqlConnection = new SqlConnection(constr);
            
            SqlCommand sqlCommand = new SqlCommand(sqlExpression, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@query", 2);

			SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();
            adapter.Fill(dataTable);
            sqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                AnswerList.Add
                    (
                        new Answer
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            Body = Convert.ToString(item["Body"]),
                            AuthorId = Convert.ToInt32(item["AuthorId"]),
                            QuestionId = Convert.ToInt32(item["QuestionId"]),
                        }
                    );
            }
            return AnswerList;
        }

        public bool AddAnswer(Answer answer)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sqlExpression, con);
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@Body", answer.Body);
            cmd.Parameters.AddWithValue("@AuthorId", answer.AuthorId);
            cmd.Parameters.AddWithValue("@QuestionId", answer.QuestionId);
            cmd.Parameters.AddWithValue("@query", 1);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool Update(Answer answer)
		{
			SqlConnection con = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, con);
			cmd.CommandType = CommandType.StoredProcedure;


			cmd.Parameters.AddWithValue("@Id", answer.Id);
			cmd.Parameters.AddWithValue("@Body", answer.Body);
			cmd.Parameters.AddWithValue("@AuthorId", answer.AuthorId);
			cmd.Parameters.AddWithValue("@QuestionId", answer.QuestionId);
			cmd.Parameters.AddWithValue("@query", 3);


			con.Open();
			int i = cmd.ExecuteNonQuery();
			con.Close();
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
			SqlConnection con = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, con);
			cmd.CommandType = CommandType.StoredProcedure;


			cmd.Parameters.AddWithValue("@Id", id);
			cmd.Parameters.AddWithValue("@query", 4);


			con.Open();
			int i = cmd.ExecuteNonQuery();
			con.Close();
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
