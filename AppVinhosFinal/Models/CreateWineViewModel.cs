using System.ComponentModel.DataAnnotations;

namespace AppVinhosFinal.Models
{
    public class CreateWineViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome do Vinho")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser ≥ 0")]
        [Display(Name = "Quantidade Total")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Quantidade Fria é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade Fria deve ser ≥ 0")]
        [Display(Name = "Quantidade Fria")]
        public int QuantidadeFria { get; set; }

        [Display(Name = "Quinta")]
        public int? QuintaId { get; set; }
    }
}
