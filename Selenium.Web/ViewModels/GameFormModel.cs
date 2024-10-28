using System.ComponentModel.DataAnnotations;

public class GameFormModel
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Range(1, 10, ErrorMessage = "Valoración debe estar entre 1 y 10.")]
    public int Rate { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int GameId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "Age no puede ser negativo.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int PaisId { get; set; }
}
