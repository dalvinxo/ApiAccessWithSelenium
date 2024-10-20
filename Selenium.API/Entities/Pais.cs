namespace Selenium.API.Entities;

public class Pais
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Relación con Persona
    public ICollection<Person> People { get; set; }
}
