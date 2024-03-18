namespace IntelligentHouse.Models;

public class House
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public Address Address { get; set; }

    [Required]
    public string UserId { get; set; }
    
    public ICollection<Thermostat> Thermostats { get; set; }
}