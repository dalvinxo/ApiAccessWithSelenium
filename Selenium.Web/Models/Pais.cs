namespace Selenium.Web.Models;

public class Pais
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Estado { get; set; } = true;
    public ICollection<Person> Persons { get; set; }
}