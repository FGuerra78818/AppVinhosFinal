using AppVinhosFinal.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AppVinhosFinal.Entities
{
    [Index(nameof(UserName), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]

        public string Password { get; set; }

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // e.g., "Admin", "User" or "Staff"

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool MustChangePassword { get; set; } = true;

        [AllowNull]
        public int? QuintaId { get; set; }

        [ForeignKey(nameof(QuintaId))]
        public Quintas? Quinta { get; set; }
    }
}
