using Microsoft.Data.SqlClient;
using System.Data;

namespace Test_qasysapp.Models.Repository
{
	public class VoteRepos
	{
		public List<Vote> VoteList { get; set; }

		public Data db = new Data();
		public string constr { get; set; }
		private string sqlExpression = "crud_vote";

		public VoteRepos()
		{
			constr = db.ConnStr!;
			VoteList = new List<Vote>();
		}

		public async Task<List<Vote>> GetVotes()
		{
			SqlConnection sqlConnection = new SqlConnection(constr);

			SqlCommand sqlCommand = new SqlCommand(sqlExpression, sqlConnection);

			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@query", 2);

			SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
			DataTable dataTable = new DataTable();

			await sqlConnection.OpenAsync();
			adapter.Fill(dataTable);
			sqlConnection.Close();

			foreach (DataRow item in dataTable.Rows)
			{
				 VoteList.Add
					(
						new Vote
						{
							Id = Convert.ToInt32(item["Id"]),
							VoterId = Convert.ToInt32(item["VoterId"]),
							AnswerId = Convert.ToInt32(item["AnswerId"]),
							VoteStatus = Convert.ToInt32(item["Score"])
						}
					);
			}
			return VoteList;
		}
		

		public bool AddVote(Vote vote)
		{
			SqlConnection con = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@VoterId", vote.VoterId);
			cmd.Parameters.AddWithValue("@AnswerId", vote.AnswerId);
			cmd.Parameters.AddWithValue("@Score", vote.VoteStatus);
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

		public bool Update(Vote vote)
		{
			SqlConnection con = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@Id", vote.Id);
			cmd.Parameters.AddWithValue("@VoterId", vote.VoterId);
			cmd.Parameters.AddWithValue("@AnswerId", vote.AnswerId);
			cmd.Parameters.AddWithValue("@Score", vote.VoteStatus);
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
