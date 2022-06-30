using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Clients;
using UserAuthentication.Models;

namespace UserAuthentication.Controllers;

[ApiController]
[Route("api/{controller}")]
public class GameController : ControllerBase
{
    private readonly GamesClient _gamesClient;
    
    public GameController(GamesClient gameClient)
    {
        _gamesClient = gameClient;
    }
    
    [HttpGet]
    [Route("getGameList")] // api/game/getGameList
    public async Task<GameListModel> GetListGames(string ordering, string? search)
    {
        var games = await _gamesClient.GetGameList(ordering, search);
        return games;
    }
    
    // [HttpGet]
    // [Route("getGameFreeToPlayList")] // api/game/getGameFreeToPlayList
    // public async Task<List<FreeToPlayGame>> GetListGames(int limit)
    // {
    //     var games = await _gamesClient.GetFreeToPlayGameList(limit);
    //     return games;
    // }

    [HttpGet]
    [Route("getGameDetails/{id}")] // api/game/getGameDetails/id
    public async Task<GameDetailsModel> GetGameDetails(int id)
    {
        var game = await _gamesClient.GetGameDetails(id);
        return game;
    }
}
