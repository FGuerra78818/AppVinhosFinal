using System.ComponentModel.DataAnnotations;

namespace AppVinhosFinal.Models
{
    public class ProfileViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Nome de Utilizador")]
        public string NewUserName { get; set; }
    }
}
