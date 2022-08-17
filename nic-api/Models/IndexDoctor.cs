using System.ComponentModel.DataAnnotations;

namespace nic_api.Models;

public class IndexDoctor
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Fio { get; set; } = string.Empty;

    [Required]
    public IndexOffice Office { get; set; } = null!;

    [Required]
    public IndexSpecialization Specialization { get; set; } = null!;

    public IndexArea? Area { get; set; }
}
