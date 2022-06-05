using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models; 

public class Player {
    // Please look at the modelbuilder for the primary key
    // Name and ShirtNumber has been selected as composite key

   [Required, MaxLength(50)]
    public string Name { get; set; }

    [Range(1,99)]
    public int ShirtNumber { get; set; }
    public decimal Salary { get; set; }
    public int GoalsThisSeason { get; set; }
  [Required]
    public string Position { get; set; }


}