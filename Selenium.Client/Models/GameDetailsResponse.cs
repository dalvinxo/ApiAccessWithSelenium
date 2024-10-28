namespace Selenium.Client.Models;

public class GameDetailsResponse
{
    public int Id { get; set; }
    public string title { get; set; }
    public string thumbnail { get; set; }
    public string status { get; set; }
    public string short_description { get; set; }
    public string description { get; set; }
    public string game_url { get; set; }
    public string genre { get; set; }
    public string platform { get; set; }
    public string publisher { get; set; }
    public string developer { get; set; }
    public string release_date { get; set; }
    public virtual Requisitos minimum_system_requirements { get; set; }

}

public class Requisitos
{

    public string os { get; set; }
    public string procesor { get; set; }
    public string memory { get; set; }
    public string graphics { get; set; }
    public string storage { get; set; }

}