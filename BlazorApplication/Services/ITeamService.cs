using WebApplication1.Models;

namespace BlazorApplication.Services; 

public interface ITeamService {
    Task AddAsync(Team newTeamItem);
    Task<List<Team>> GetAllTeamAsync();
    Task<List<Team>?> GetTeamsByFilter
        (int ratingToSearch, string titleToSearch);
}