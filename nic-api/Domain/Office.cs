using System.ComponentModel.DataAnnotations;

namespace nic_api.Domain;

public class Office
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string Number { get; set; } = string.Empty;
}
