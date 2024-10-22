namespace Selenium.Web.Models;

public class Post
{
    public int Id { get; set; }
    public string Comment { get; set; }

    // Valoración (del 1 al 10, por ejemplo)
    public int Rate { get; set; }

    public DateTime Fecha { get; set; }


    public int IdGame { get; set; }
    public Game Game { get; set; }


    public int IdPerson { get; set; }
    public Person Person { get; set; }
}
