using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppVinhosFinal.Models
{
    public class PedidoVinho
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPedido { get; set; }

        [ForeignKey(nameof(IdPedido))]
        public Pedidos? Pedido { get; set; }

        [Required]
        public int IdVinho { get; set; }

        [ForeignKey(nameof(IdVinho))]
        public Vinhos? Vinho { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public TipoVinho Tipo { get; set; } = TipoVinho.Frio;
    }

    public enum TipoVinho
    {
        Frio = 0,
        Quente = 1
    }
}
