using Newtonsoft.Json;
using UserAuthentication.Models.EsportModels;
using UserAuthentication.Utils;

namespace UserAuthentication.Clients;

public class EsportClient
{
    private HttpClient _client;
    private static string _address;
    private static string _key;
    private static string _headerSite;
    private static string _headerKey;

    public EsportClient()
    {
        _address = Constants.ESportUrl;
        _key = Constants.ESportKey;

        _client = new HttpClient();
        _client.BaseAddress = new Uri(_address);

        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_key}");
    }
    
    public async Task<List<TournamentListModel>> getUpcomingTournaments()
    {
        var response = await _client.GetAsync($"/tournaments/upcoming");
        response.EnsureSuccessStatusCode();
        
        var content = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<List<TournamentListModel>>(content);

        return result!;
    }
}
