using System.Net;
using System.Text;
using System.Text.Json;
using BlazorApplication.Services;
using WebApplication1.Models;

namespace BlazorApplication.HttpClients; 

public class TeamHttpClient : ITeamService {
    public async Task AddAsync(Team newTeamItem) {
        using HttpClient client = new HttpClient();
        string teamAsJson = JsonSerializer.Serialize(newTeamItem);
        StringContent content = new StringContent(teamAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync($"http://localhost:5006/Teams", content);
        String responseContent = await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
    }

    public async Task<List<Team>> GetAllTeamAsync() {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"http://localhost:5006/Teams");
        String responseContent = await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        List<Team> allTeams = GetDeserialized<List<Team>>(responseContent);
        return allTeams;



    }

    public async Task<List<Team>?> GetTeamsByFilter(int ratingToSearch, string titleToSearch) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"http://localhost:5006/Teams?teamName={titleToSearch}&&ranking={ratingToSearch}");
        String responseContent = await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        List<Team> allTeams = GetDeserialized<List<Team>>(responseContent);
        return allTeams;
    }

    private T GetDeserialized<T>(string jsonFormat) {
        T obj = JsonSerializer.Deserialize<T>(jsonFormat, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }) !;
        return obj;
    }
    private async Task<string> GetResponseContentFromResponseMessageAndThrowAppropriateException(
        HttpResponseMessage responseMessage) {
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error :{responseMessage.StatusCode}, {responseContent}");
        }

        return responseContent;
    }
}