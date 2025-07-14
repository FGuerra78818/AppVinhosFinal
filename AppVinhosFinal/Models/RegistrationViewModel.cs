using AppVinhosFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AppVinhosFinal.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // e.g., "Admin", "User", "Staff" or "CEO"

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [AllowNull]
        public int? QuintaId { get; set; }
    }
}
