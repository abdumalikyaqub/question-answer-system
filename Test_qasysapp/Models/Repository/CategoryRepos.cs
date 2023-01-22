using Microsoft.Data.SqlClient;
using System.Data;

namespace Test_qasysapp.Models.Repository
{
    public class CategoryRepos
    {

        public List<Category> CategoryList { get; set; }
		public Data db = new Data();
		public string constr { get; set; }
		private string sqlExpression = "crud_category";
		public CategoryRepos()
        {
            constr = db.ConnStr!;
            CategoryList = new List<Category>();
        }
        public List<Category> GetCategories()
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
                CategoryList.Add
                    (
                        new Category
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            Name = Convert.ToString(item["Name"]),
                        }
                    );
            }
            return CategoryList;
        }

		public bool AddCat(Category category)
		{
			SqlConnection con = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, con);
			cmd.CommandType = CommandType.StoredProcedure;


			cmd.Parameters.AddWithValue("@Name", category.Name);
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
		public bool Update(Category category)
		{
			SqlConnection con = new SqlConnection(constr);
			SqlCommand cmd = new SqlCommand(sqlExpression, con);
			cmd.CommandType = CommandType.StoredProcedure;


			cmd.Parameters.AddWithValue("@Id", category.Id);
			cmd.Parameters.AddWithValue("@Name", category.Name);
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
