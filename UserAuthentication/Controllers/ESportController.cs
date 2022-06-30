using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Clients;
using UserAuthentication.Models.EsportModels;

namespace UserAuthentication.Controllers;

[ApiController]
[Route("api/{controller}")]
public class ESportController : ControllerBase
{
    private readonly EsportClient _esportClient;
    
    public ESportController(EsportClient esportClient)
    {
        _esportClient = esportClient;
    }

    [HttpGet]
    [Route("getUpcomingTournaments")] // api/esport/getUpcomingTournaments
    public async Task<List<TournamentListModel>> GetUpcomingTournaments()
    {
        var tournaments = await _esportClient.getUpcomingTournaments();

        return tournaments;
    }
}
