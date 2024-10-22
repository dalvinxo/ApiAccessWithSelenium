namespace Selenium.API.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public int Edad { get; set; }


    public int PaisId { get; set; }
    public Pais Pais { get; set; }

    // Relación con Post
    public ICollection<Post> Posts { get; set; }
}
