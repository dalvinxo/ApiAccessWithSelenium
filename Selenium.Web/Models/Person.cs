namespace Selenium.Web.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Cedula { get; set; }
    public string? Telefono { get; set; }
    public string Genre { get; set; }
    public int Age { get; set; }
    public int PaisId { get; set; }
    public Pais Pais { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<Post>? Posts { get; set; }

}
