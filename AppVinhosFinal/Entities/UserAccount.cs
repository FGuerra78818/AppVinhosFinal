using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System;

namespace AppVinhosFinal.Entities
{
    public class UserAccount : IdentityUser<int>
    {
        public string Role { get; set; } = "User"; // e.g., "Admin", "User", "Staff" or "CEO"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool MustChangePassword { get; set; } = true;

        [AllowNull]
        public int? QuintaId { get; set; }

        [ForeignKey(nameof(QuintaId))]
        public Quintas? Quinta { get; set; }
    }
}
