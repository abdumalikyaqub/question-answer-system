using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Test_qasysapp.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string? Body { get; set; }
        public int QuestionId { get; set; }
        public int AuthorId { get; set; }

    }
}
