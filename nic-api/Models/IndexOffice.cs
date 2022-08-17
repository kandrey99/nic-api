using System.ComponentModel.DataAnnotations;

namespace nic_api.Models;

public class IndexOffice
{
    [Required]
    [MaxLength(255)]
    public string Number { get; set; } = string.Empty;
}
