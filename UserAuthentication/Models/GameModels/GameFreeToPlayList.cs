namespace UserAuthentication.Models;

public class GameFreeToPlayList
{
    public FreeToPlayGame Games { get; set; }
}

public class FreeToPlayGame
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Short_description { get; set; }
    public string Game_url { get; set; }
    public string Genre { get; set; }
    public string Platform { get; set; }
    public string Release_date { get; set; }
    public string Developer { get; set; }
}
