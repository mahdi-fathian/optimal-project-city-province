using System.ComponentModel.DataAnnotations;

namespace Project.DTOs;

public class ProvinceDto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public int CitiesCount { get; set; }
}

public class CreateProvinceDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}

public class UpdateProvinceDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
