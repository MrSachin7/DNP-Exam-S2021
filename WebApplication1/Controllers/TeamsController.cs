using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase {
    private readonly Context _context;

    public TeamsController(Context context) {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<Team>> AddTeamAsync([FromBody] Team team) {
        try {
            EntityEntry<Team> entry = _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return Created($"/{entry.Entity.TeamName}", entry.Entity);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Team>>> GetTeams([FromQuery] int? ranking, [FromQuery] string? teamName) {
        // try {
        //     if (ranking ==null && teamName ==null) {
        //         List<Team> teamsFromDb = await _context.Teams.ToListAsync();
        //         return Ok(teamsFromDb);
        //     }
        //
        //     if (ranking ==null) {
        //         List<Team> teamsFromDb =
        //             await _context.Teams.Where(team => team.TeamName.Contains(teamName!)).ToListAsync();
        //         return Ok(teamsFromDb);
        //     }
        //
        //     if (teamName ==null) {
        //         List<Team> teamsFromDb = await _context.Teams.Where(team => team.Ranking <= ranking).ToListAsync();
        //         return Ok(teamsFromDb);
        //     }
        //
        //     List<Team> teamsFromDatabase = await _context.Teams
        //         .Where(team => team.Ranking <= ranking && team.TeamName.Contains(teamName)).ToListAsync();
        //     return Ok(teamsFromDatabase);
        // }
        // catch (Exception e) {
        //     return StatusCode(500, e.Message);
        // }
        try {
            IQueryable<Team> queryable = _context.Teams;

            if (ranking != null) {
                queryable = queryable.Where(team => team.Ranking <= ranking);
            }

            if (teamName != null) {
                queryable = queryable.Where(team => team.TeamName.Contains(teamName));
            }

            List<Team> teams = await queryable.ToListAsync();
            return Ok(teams);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}