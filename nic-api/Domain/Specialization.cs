using System.ComponentModel.DataAnnotations;

namespace nic_api.Domain;

public class Specialization
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; } = null!;
}
