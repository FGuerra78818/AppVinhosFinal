using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AppVinhosFinal.Models
{
    public class CreateWineViewModel : IValidatableObject
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantidade { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int QuantidadeFria { get; set; }

        // Só preenchido por Admin/Staff via dropdown
        public int? QuintaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (QuantidadeFria > Quantidade)
            {
                yield return new ValidationResult(
                    "A quantidade fria não pode ser superior à quantidade total.",
                    new[] { nameof(QuantidadeFria) }
                );
            }
        }
    }
}
