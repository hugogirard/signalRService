namespace IntelligentHouse.Models;
public class Address
{
    [Key]
    public int Id { get; set; }
    
    public string CivicNumber { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string Street { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string City { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(50)]
    public string State { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(6)]
    public string ZipCode { get; set; }
    public House House { get; set; }
}