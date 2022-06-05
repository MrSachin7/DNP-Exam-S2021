using System.Net;
using System.Text;
using System.Text.Json;
using BlazorApplication.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApplication1.Models;

namespace BlazorApplication.HttpClients; 

public class PlayerHttpClient : IPlayerService {
    public async Task AddPlayerAsync(Player newPlayerItem, string selectedteam) {
        using HttpClient client = new HttpClient();
        string playerAsJson = JsonSerializer.Serialize(newPlayerItem);
        StringContent content = new StringContent(playerAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync($"http://localhost:5006/Players/{selectedteam}", content);
        string response = await        GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
    }

    public async Task<Player> RemovePlayerAsync(string playerName, int shirtNumber) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"http://localhost:5006/Players/{playerName}/{shirtNumber}");
        string response = await  GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Player player =  GetDeserialized<Player>(response);
        return player;


    }

    private async Task<string> GetResponseContentFromResponseMessageAndThrowAppropriateException(
        HttpResponseMessage responseMessage) {
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error :{responseMessage.StatusCode}, {responseContent}");
        }

        return responseContent;
    }
    private T GetDeserialized<T>(string jsonFormat) {
        T obj = JsonSerializer.Deserialize<T>(jsonFormat, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }) !;
        return obj;
    }
}