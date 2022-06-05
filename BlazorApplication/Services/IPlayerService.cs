using WebApplication1.Models;

namespace BlazorApplication.Services; 

public interface IPlayerService {
    Task AddPlayerAsync(Player newPlayerItem, string selectedteam);
}