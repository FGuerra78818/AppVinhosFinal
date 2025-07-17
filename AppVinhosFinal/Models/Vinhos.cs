using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppVinhosFinal.Models
{
    public class Vinhos
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public int QuantidadeFria { get; set; }

        [Required]
        public int CapacidadeFria { get; set; }

        [Required]
        public int QuantidadeQuente
        {
            get => Quantidade - QuantidadeFria;
        }

        [Required]
        public int IdQuinta { get; set; }

        [ForeignKey("IdQuinta")]
        public Quintas? Quinta { get; set; }

        public List<PedidoVinho> PedidoVinhos { get; set; } = new();

        [Required]
        public EstadoVinho Estado { get; set; } = EstadoVinho.Visible;
    }

    public enum EstadoVinho
    {
        Visible = 0,
        Hidden = 1
    }
}
