using Fantasy.Shared.Entites.Gestion;
using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.Entites.GeneralParameters
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El maximo número de caracteres es {0}")]
        [Required]
        public string? Name { get; set; } = null!;

        public ICollection<Team>? Teams { get; set; }

        public int TeamsCount => Teams == null ? 0 : Teams.Count;
    }
}