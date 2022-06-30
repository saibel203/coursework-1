namespace UserAuthentication.Models;

public class GameDetailsModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Metacritic { get; set; }
    public string Released { get; set; }
    public string Background_image { get; set; }
    public string Website { get; set; }
    public string Metacritic_url { get; set; }
    public List<ParentPlatform> Parent_platforms { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Tag> Tags { get; set; }
}

