using System.ComponentModel.DataAnnotations;

namespace nic_api.Models;

public class IndexSpecialization
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
}
