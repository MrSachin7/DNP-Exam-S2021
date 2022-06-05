using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models; 

public class Team {
   [Key]
    public string TeamName { get; set; }
   [Required, MaxLength(50)]
    public string NameOfCoach { get; set; }

    public ICollection<Player> Players { get; set; }


    public int Ranking { get; set; }
}