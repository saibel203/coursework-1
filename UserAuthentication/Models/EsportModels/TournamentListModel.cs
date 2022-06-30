namespace UserAuthentication.Models.EsportModels;

public class TournamentListModel
{
    public int Id { get; set; }
    public string Begin_at { get; set; }
    public string End_at { get; set; }
    public League League { get; set; }
    public List<Match> Matches { get; set; }
    public VideoGame Videogame { get; set; }
}

public class TournamentList
{
    
}

public class VideoGame
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class League
{
    public int Id { get; set; }
    public string Image_url { get; set; }
    public string Name { get; set; }
}

public class Match
{
    public int Id { get; set; }
    public string Begin_at { get; set; }
    public string Name { get; set; }
    public int Number_of_games { get; set; }
}
