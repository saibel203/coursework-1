using Newtonsoft.Json;
using UserAuthentication.Models;
using UserAuthentication.Utils;

namespace UserAuthentication.Clients;

public class GamesClient
{
    private HttpClient _client;
    private static string _address;
    private static string _key;
    private static string _headerSite;
    private static string _headerKey;

    public GamesClient()
    {
        _address = Constants.GameUrl;
        _key = Constants.GameKey;

        _client = new HttpClient();
        _client.BaseAddress = new Uri(_address);

        _client.DefaultRequestHeaders.Add("x-rapidapi-host", Constants.RapidGameUrl);
        _client.DefaultRequestHeaders.Add("x-rapidapi-key", Constants.RapidApiKey);
    }

    public async Task<GameListModel> GetGameList(string ordering, string? search)
    {
        var response = await _client.GetAsync($"api/games?key={_key}&page_size=21&search={search}&ordering={ordering}");
        response.EnsureSuccessStatusCode();
        
        var content = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<GameListModel>(content);

        return result!;
    }
    
    // public async Task<List<FreeToPlayGame>> GetFreeToPlayGameList(int limit)
    // {
    //     var response = await _client.GetAsync($"api/games?key={_key}&limit={limit}");
    //     response.EnsureSuccessStatusCode();
    //     
    //     var content = response.Content.ReadAsStringAsync().Result;
    //
    //     var result = JsonConvert.DeserializeObject<List<FreeToPlayGame>>(content);
    //
    //     return result!;
    // }
    
    public async Task<GameDetailsModel> GetGameDetails(int id)
    {
        var response = await _client.GetAsync($"api/games/{id}?key={_key}");
        response.EnsureSuccessStatusCode();
        
        var content = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<GameDetailsModel>(content);

        return result!;
    }
}
