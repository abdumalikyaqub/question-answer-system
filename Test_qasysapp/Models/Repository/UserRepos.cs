using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace Test_qasysapp.Models.Repository
{
    public class UserRepos
    {
        public List<User> UsersList { get; set; }
        public Data db = new Data();
        public string? constr { get; set; }
		private string sqlExpression = "crud_user";
		public UserRepos()
        {
            constr = db.ConnStr!;
            UsersList = new List<User>();
        }
        public List<User> GetUsers()
        {
            SqlConnection sqlConnection= new SqlConnection(constr);

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
                UsersList.Add
                    (
                        new User
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            FullName = Convert.ToString(item["FullName"]),
                            Email = Convert.ToString(item["Email"]),
                            Password = Convert.ToString(item["Password"]),
                            Login = Convert.ToString(item["Login"]),
                            RoleId = Convert.ToInt32(item["RoleId"]),
                        }
                    );
            }
            return UsersList;
        }

        public int GetRating(int userid)
        {
            int score = 0;
            SqlConnection sqlConnection = new SqlConnection(constr);
            var sqlreq = "select  dbo.Rating(@userid)";

            SqlCommand sqlCommand = new SqlCommand(sqlreq, sqlConnection);
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@userid", userid);

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlConnection.Open();
            adapter.Fill(dataTable);
            sqlConnection.Close();

            score = (int)dataTable.Rows[0][0];
            //foreach (DataRow item in dataTable.Rows)
            //{
            //    score = Convert.ToInt32(item["score"]);
            //}
            return score;
        }


        public bool AddUser(User user)
        {
            SqlConnection sqlConnection = new SqlConnection(constr);
            
            SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FullName", user.FullName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Login", user.Login);
            cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
            cmd.Parameters.AddWithValue("@query", 1);

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

		public bool UpdateUser(User user)
		{
			SqlConnection sqlConnection = new SqlConnection(constr);
			
			SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@Id", user.Id);
			cmd.Parameters.AddWithValue("@FullName", user.FullName);
			cmd.Parameters.AddWithValue("@Email", user.Email);
			cmd.Parameters.AddWithValue("@Password", user.Password);
			cmd.Parameters.AddWithValue("@Login", user.Login);
			cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
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

		public bool DeleteUser(int id)
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
