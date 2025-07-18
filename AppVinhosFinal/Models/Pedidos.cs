using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppVinhosFinal.Models
{
    public class Pedidos
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.Now;

        public DateTime? DataAprovacao { get; set; }

        [Required]
        public EstadoPedido Estado { get; set; } = EstadoPedido.PorAprovar;

        public List<PedidoVinho> PedidoVinhos { get; set; } = new();
    }

    public enum EstadoPedido
    {
        PorAprovar = 0,
        Aprovado = 1,
        Cancelado = 2
    }
}
