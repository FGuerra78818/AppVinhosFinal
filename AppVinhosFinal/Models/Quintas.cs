using AppVinhosFinal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppVinhosFinal.Models
{
    [Index(nameof(Nome), IsUnique = true)]
    public class Quintas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int NumeroMaxVinhoFrio { get; set; } = 0;

        public List<Vinhos>? Vinhos { get; set; } = new();

        public List<UserAccount>? UserAccounts { get; set; } = new();
    }
}

