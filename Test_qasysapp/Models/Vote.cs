using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_qasysapp.Models
{
	public class Vote
	{
		[Key]
		public int Id { get; set; }
		public int VoterId { get; set; }

		[ForeignKey(nameof(VoterId))]
		public User Users { get; set; } = null!;

		public int AnswerId { get; set; }

		[ForeignKey(nameof(AnswerId))]
		public Answer Answers { get; set; } = null!;

		public int VoteStatus { get; set; }
/*
		public DateTime CreatedAt { get; set; } */
	}
}
