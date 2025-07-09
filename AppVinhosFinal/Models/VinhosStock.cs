using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppVinhosFinal.Models
{
    public class VinhosStock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdVinho { get; set; }

        [ForeignKey(nameof(IdVinho))]
        public Vinhos? Vinho { get; set; }

        [Required]
        public EstadoVinhoStock Estado { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }

    public enum EstadoVinhoStock
    {
        Quente = 0,
        Frio = 1
    }
}
