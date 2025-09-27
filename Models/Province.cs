using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class Province
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
