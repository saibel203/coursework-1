namespace UserAuthentication.Models;

public class GameListModel
{
    public List<Game> Results { get; set; }
}

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Released { get; set; }
    public string Background_image { get; set; }
    public int? Metacritic { get; set; }
    public List<ParentPlatform> Parent_platforms { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Tag> Tags { get; set; }
}


public class ParentPlatform
{
    public Platform Platform { get; set; }
}

public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
}