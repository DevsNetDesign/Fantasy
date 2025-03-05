using Fantasy.Shared.Entites.GeneralParameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Shared.Entites.Gestion;

public class Team
{
    public int Id { get; set; }

    [MaxLength(100, ErrorMessage = "El maximo número de caracteres es {0}")]
    [Required]
    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public Country Country { get; set; } = null!;

    public int CountryId { get; set; }
}