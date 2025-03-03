using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.Entites.GeneralParameters
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El maximo número de caracteres es {0}")]
        [Required]
        public string? Name { get; set; }
    }
}