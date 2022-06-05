using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayersController : ControllerBase {
    private readonly Context _context;

    public PlayersController(Context context) {
        _context = context;
    }


    [HttpPost]
    [Route("{teamName}")]
    public async Task<ActionResult<Player>> AddPlayerAsync([FromBody] Player player, [FromRoute] string teamName) {
        try {
            Team? team = await _context.Teams.Include(team1 => team1.Players).FirstOrDefaultAsync(team1 => team1.TeamName.Equals(teamName));
            if (team == null) {
                return StatusCode(500, "Team to be added not found");
            }

            team.Players!.Add(player);
            EntityEntry<Player> entry = await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            Player addedPlayer = entry.Entity;
            return Created($"/{addedPlayer.Name}/{addedPlayer.ShirtNumber}", addedPlayer);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{name}/{shirtNumber:int}")]
    public async Task<ActionResult<Player>> DeletePlayerAsync([FromRoute] string name, [FromRoute] int shirtNumber) {
        Player? player = await _context.Players.FirstOrDefaultAsync(player1 =>
            player1.Name.Equals(name) && player1.ShirtNumber == shirtNumber);
        if (player == null) {
            return StatusCode(500, "Player not found in database");
        }

        EntityEntry<Player> remove = _context.Players.Remove(player);
        await _context.SaveChangesAsync();
        return Ok(remove.Entity);
    }
}