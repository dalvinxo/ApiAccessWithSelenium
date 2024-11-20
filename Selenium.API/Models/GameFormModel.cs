using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class GameFormModel
{

    [DisplayName("Título")]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public required string Title { get; set; }

    [DisplayName("Descripción")]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public required string Description { get; set; }

    [DisplayName("Valoración del juego")]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Range(1, 10, ErrorMessage = "Valoración debe estar entre 1 y 10.")]
    public int Rate { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int GameId { get; set; }

    [DisplayName("Usuario")]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Name { get; set; }

    [DisplayName("Género")]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Genre { get; set; }

    [DisplayName("Edad")]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "La edad no puede ser negativo.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int PaisId { get; set; }
}
