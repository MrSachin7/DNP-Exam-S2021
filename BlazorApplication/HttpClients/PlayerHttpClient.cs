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
    private async Task<string> GetResponseContentFromResponseMessageAndThrowAppropriateException(
        HttpResponseMessage responseMessage) {
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error :{responseMessage.StatusCode}, {responseContent}");
        }

        return responseContent;
    }
}