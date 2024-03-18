namespace IntelligentHouse.Models;
public class Thermostat
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public int Temperature { get; set; }

    [Required]
    public int HouseId { get; set; }
    
    public House Location { get; set; }
}
