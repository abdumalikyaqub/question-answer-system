namespace Test_qasysapp.Models.Repository
{
	public class Data
	{
		public  string? ConnStr { get; set; }
		

		public Data()
		{
			ConnStr = "Server=COMPUTER\\SQLEXPRESS;Database=dbqasystem;" +
				"Trusted_Connection=True;TrustServerCertificate=True";
		}



	}
}
