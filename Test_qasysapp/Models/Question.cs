using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Test_qasysapp.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
