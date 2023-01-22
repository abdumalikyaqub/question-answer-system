using System.ComponentModel.DataAnnotations;

namespace Test_qasysapp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required!")]
        public string? FullName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
        public string? Login { get; set; }
        public int RoleId { get; set; }
    }
}
