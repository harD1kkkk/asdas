using System.ComponentModel.DataAnnotations;

namespace Project_Coffe.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }
    }
}
