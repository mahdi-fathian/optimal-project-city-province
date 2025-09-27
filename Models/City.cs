using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project.Models;

public class City
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int ProvinceId { get; set; }

    [ForeignKey("ProvinceId")]
    [JsonIgnore]
    public virtual Province? Province { get; set; }

    public bool IsCenterOfProvince { get; set; }

    [JsonIgnore]
    public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
}
