using System.ComponentModel.DataAnnotations;

namespace Project.DTOs;

public class CityDto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public int ProvinceId { get; set; }
    
    public string? ProvinceName { get; set; }
    
    public bool IsCenterOfProvince { get; set; }
}

public class CreateCityDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public int ProvinceId { get; set; }
    
    public bool IsCenterOfProvince { get; set; }
}

public class UpdateCityDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public int ProvinceId { get; set; }
    
    public bool IsCenterOfProvince { get; set; }
}
