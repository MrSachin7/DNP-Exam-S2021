using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DataAccess; 

public class Context : DbContext{
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite(
            @"Data Source =D:\SEM 3\DNP1\DNP-Exam-S2021\WebApplication1\database.db");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        // Setting the PK of the player to the composite of name and shirtNumber
        modelBuilder.Entity<Player>().HasKey(player => new {player.Name, player.ShirtNumber});
    }
}