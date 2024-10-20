namespace Selenium.API.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }  // Puede ser el género de la persona (hombre, mujer, etc.)
    public int Edad { get; set; }
    
    // Relación con País
    public int PaisId { get; set; }
    public Pais Pais { get; set; }
    
    // Relación con Post
    public ICollection<Post> Posts { get; set; }
}
