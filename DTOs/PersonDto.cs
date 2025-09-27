using System.ComponentModel.DataAnnotations;

namespace Project.DTOs;

public class PersonDto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public int CityId { get; set; }
    
    public string? CityName { get; set; }
    public string? ProvinceName { get; set; }
    
    [Required]
    public string Gender { get; set; } = string.Empty;
}

public class CreatePersonDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public int CityId { get; set; }
    
    [Required]
    public string Gender { get; set; } = string.Empty;
}

public class UpdatePersonDto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public int CityId { get; set; }
    
    [Required]
    public string Gender { get; set; } = string.Empty;
}
