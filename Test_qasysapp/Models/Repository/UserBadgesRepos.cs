using Microsoft.Data.SqlClient;
using System.Data;

namespace Test_qasysapp.Models.Repository
{
    public class UserBadgesRepos
    {
        private List<UserBadges> badges = new List<UserBadges>();

        public Data db = new Data();
        public string constr { get; set; }
        private string sqlExpression = "crud_userbadges";

        public UserBadgesRepos()
        {
            constr = db.ConnStr!;
        }

        public List<UserBadges> GetAll()
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

                badges.Add(

                    new UserBadges
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        BadgeId = Convert.ToInt32(dr["BadgeId"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                    }
                    );
            }
            return badges;
        }

        public bool AddBadge(UserBadges badge)
        {


            SqlConnection sqlConnection = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sqlExpression, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BadgeId", badge.BadgeId);
            cmd.Parameters.AddWithValue("@UserId", badge.UserId);
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
    }
}
