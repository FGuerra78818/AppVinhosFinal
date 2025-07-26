using System.ComponentModel.DataAnnotations;

namespace AppVinhosFinal.Models
{
    public class LogsCopos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuantidadeVendida { get; set; }

        [Required]
        public DateTime DataHoraVenda { get; set; } = DateTime.UtcNow;

        public string? Comprador { get; set; }
    }
}
